// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.TemplateEngine.Abstractions;
using Microsoft.TemplateSearch.Common.Abstractions;
using Microsoft.TemplateSearch.TemplateDiscovery.PackProviders;

namespace Microsoft.TemplateSearch.TemplateDiscovery.AdditionalData
{
    internal interface IAdditionalDataProducer
    {
        string DataUniqueName { get; }

        string Serialized { get; }

        object? Data { get; }

        void CreateDataForTemplatePack(IDownloadedPackInfo packInfo, IReadOnlyList<ITemplateInfo> templates, IEngineEnvironmentSettings environment);

        object? GetDataForPack(ITemplatePackageInfo packInfo);

        object? GetDataForTemplate(ITemplatePackageInfo packInfo, string templateIdentity);

    }
}
