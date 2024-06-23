using Domain.Entities;
using Repositories;

namespace Services.Implementation
{
    class ContactPostService(IContactPostRepository contantPostRepository) : IContactPostService
    {
        public async Task<string> Add(string fullName, string email, string message)
        {
            var post = new ContactPost
            {
                FullName = fullName,
                Email = email,
                Message = message   
            };

            await contantPostRepository.AddAsync(post);
            await contantPostRepository.SaveAsync();

            return "Müraciətiniz qəbul olundu!";
        }
    }
}
