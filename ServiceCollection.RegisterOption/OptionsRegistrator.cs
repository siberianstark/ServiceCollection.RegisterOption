using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceCollection.RegisterOption
{
    public static class OptionsRegistrator
    {
        private const string DefaultOptionsPostfix = "Options";
        
        public static IServiceCollection RegisterOptions<TOptions>(this IServiceCollection serviceCollection,
            IConfiguration configuration, string sectionName = null) where TOptions: class
        {
            if (string.IsNullOrEmpty(sectionName))
            {
                var genericClassOptionsName = nameof(TOptions);

                if (genericClassOptionsName.EndsWith(DefaultOptionsPostfix))
                {
                    genericClassOptionsName =
                        genericClassOptionsName.Remove(genericClassOptionsName.Length - DefaultOptionsPostfix.Length);
                }

                sectionName = genericClassOptionsName;
            }
            
            return serviceCollection.Configure<TOptions>(configuration.GetSection(sectionName));
        }
    }
}