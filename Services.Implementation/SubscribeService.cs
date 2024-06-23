using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Repositories;
using Services.Common;
using Services.Implementation.Common;

namespace Services.Implementation
{
    public class SubscribeService : ISubscribeService
    {
        private readonly ISubscriberRepository subscriberRepository;
        private readonly IEmailService emailService;
        private readonly ICryptoService cryptoService;
        private readonly IHttpContextAccessor ctx;

        public SubscribeService(ISubscriberRepository subscribeRepository,
            IEmailService emailService,
            ICryptoService cryptoService,
            IHttpContextAccessor ctx)
        {
            this.subscriberRepository = subscribeRepository;
            this.emailService = emailService;
            this.cryptoService = cryptoService;
            this.ctx = ctx;
        }



        public async Task<Tuple<bool, string>> Subscribe(string email)
        {
            var entity = await subscriberRepository.GetAsync(m => m.Email.Equals(email));

            var data = Tuple.Create(true, "");

            if(entity?.ApprovedAt is not null)
            {
                return Tuple.Create(true, "Siz artıq abunə olmusunuz!");
            }
            else if(entity is not null)
            {
                return Tuple.Create(true, "Siz abunəçilik üçün E-poçt adresini təsdiqləməlisiniz!");
            }


            entity = new Subscribe { Email = email };
            await subscriberRepository.AddAsync(entity);
            await subscriberRepository.SaveAsync();

            var token = $"id={entity.Email}|expire={DateTime.Now.AddHours(1):yyyy.MM.dd HH:mm:ss}";

            token = cryptoService.Encrypt(token, true);

            string redirectUrl = $"{ctx.HttpContext.Request.Scheme}://{ctx.HttpContext.Request.Host}/subscribe-approve?token={token}";

            var msg = $"Salam,<br/>Abuneliyinizi <a href=\"{redirectUrl}\">link</a>`lə tamamlayin";

            await emailService.SendEmail(entity.Email, "Ogani Subscription", msg);

            return Tuple.Create(false, "E-poçt ünvanınıza təsdiq linki göndərildi.1 saat ərzidə abunəliyinizi tamamlamağı unutmayın!");
        }

        public Task<Tuple<bool, string>> SubscribeApprove(string token)
        {
            throw new NotImplementedException();
        }
    }
}
