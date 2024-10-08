﻿using System.Reflection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace H2MLauncher.UI
{
    public static class OptionsConfigurationExtensions
    {
        public static OptionsBuilder<TOptions> BindMapConfiguration<TGlobal, TOptions>(
            this OptionsBuilder<TOptions> optionsBuilder,
            string configSectionKey,
            Func<TGlobal, TOptions> mappingFunc)
            where TGlobal : class, new()
            where TOptions : class
        {
            optionsBuilder.Configure((TOptions options, IConfiguration configuration) =>
            {
                var globalSettings = new TGlobal();
                configuration.GetSection(configSectionKey).Bind(globalSettings);
                TOptions specificOptions = mappingFunc(globalSettings);

                foreach (PropertyInfo prop in typeof(TOptions).GetProperties())
                {
                    object? value = prop.GetValue(specificOptions);
                    prop.SetValue(options, value);
                }
            });
            optionsBuilder.Services.AddSingleton<IOptionsChangeTokenSource<TOptions>, ConfigurationChangeTokenSource<TOptions>>();

            return optionsBuilder;
        }
        public static OptionsBuilder<TOptions> MapFrom<TBaseOptions, TOptions>(
            this OptionsBuilder<TOptions> optionsBuilder,
            Func<TBaseOptions, TOptions> mappingFunc)
            where TBaseOptions : class, new()
            where TOptions : class
        {
            optionsBuilder.Configure((TOptions options, IOptionsMonitor<TBaseOptions> baseOptions) =>
            {
                TOptions specificOptions = mappingFunc(baseOptions.CurrentValue);

                foreach (PropertyInfo prop in typeof(TOptions).GetProperties())
                {
                    object? value = prop.GetValue(specificOptions);
                    prop.SetValue(options, value);
                }
            });
            optionsBuilder.Services.AddSingleton<IOptionsChangeTokenSource<TOptions>, ConfigurationChangeTokenSource<TOptions>>();

            return optionsBuilder;
        }
    }
}
