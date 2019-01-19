using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceCollection.RegisterOption
{
    public static class OptionsRegistrator
    {
        private const string DefaultOptionsPostfix = "Options";

        private static string GetSectionName<TOptions>(string sectionName) where TOptions: class
        {
            if (string.IsNullOrEmpty(sectionName))
            {
                var genericClassOptionsName = typeof(TOptions).Name;

                if (genericClassOptionsName.EndsWith(DefaultOptionsPostfix))
                {
                    genericClassOptionsName =
                        genericClassOptionsName.Remove(genericClassOptionsName.Length - DefaultOptionsPostfix.Length);
                }

                sectionName = genericClassOptionsName;
            }

            return sectionName;
        }
        
        /// <summary>
        /// IServiceCollection options registration extension
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration"></param>
        /// <param name="sectionName"></param>
        /// <typeparam name="TOptions"></typeparam>
        /// <returns></returns>
        public static IServiceCollection RegisterOptions<TOptions>(this IServiceCollection serviceCollection,
            IConfiguration configuration, string sectionName = null) where TOptions: class
        {
            sectionName = GetSectionName<TOptions>(sectionName);
            return serviceCollection.Configure<TOptions>(configuration.GetSection(sectionName));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration"></param>
        /// <param name="postConfigureBinder"></param>
        /// <param name="sectionName"></param>
        /// <typeparam name="TOptions"></typeparam>
        /// <returns></returns>
        public static IServiceCollection RegisterOptions<TOptions>(this IServiceCollection serviceCollection,
            IConfiguration configuration, Action<TOptions> postConfigureBinder, string sectionName = null) where TOptions: class
        {
            sectionName = GetSectionName<TOptions>(sectionName);
            serviceCollection.Configure<TOptions>(configuration.GetSection(sectionName));
            serviceCollection.PostConfigure<TOptions>(postConfigureBinder);

            return serviceCollection;
        }
    }
}