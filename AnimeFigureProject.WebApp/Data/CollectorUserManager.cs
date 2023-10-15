using AnimeFigureProject.DatabaseAccess;
using AnimeFigureProject.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AnimeFigureProject.WebApp.Data
{

    public class CollectorUserManager : UserManager<IdentityUser>
    {

        private readonly DataAccessService dataAccessService;

        public CollectorUserManager(DataAccessService dataAccessService, IUserStore<IdentityUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<IdentityUser> passwordHasher, IEnumerable<IUserValidator<IdentityUser>> userValidators, IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<IdentityUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger) 
        {

            this.dataAccessService = dataAccessService;

        }

        public override async Task<IdentityResult> CreateAsync(IdentityUser user)
        {

            var Result = await base.CreateAsync(user);

            if (Result.Succeeded) 
            {

                Collector collector = new Collector
                {

                    Name = user.UserName,
                    AuthenticationUserId = user.Id

                };

                await dataAccessService.CreateCollector(collector);
            
            }

            return Result;

        }

    }

}
