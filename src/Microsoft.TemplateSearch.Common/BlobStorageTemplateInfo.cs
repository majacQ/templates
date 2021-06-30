﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TemplateEngine;
using Microsoft.TemplateEngine.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.TemplateSearch.Common
{
    [JsonObject(Id = "TemplateInfo")]
    public class BlobStorageTemplateInfo : ITemplateInfo
    {
        public BlobStorageTemplateInfo(ITemplateInfo templateInfo)
        {
            Identity = templateInfo.Identity;
            Name = templateInfo.Name;
            ShortNameList = templateInfo.ShortNameList;
            Parameters = templateInfo.Parameters.Select(p => new BlobTemplateParameter(p)).ToList();
            Author = templateInfo.Author;
            Classifications = templateInfo.Classifications;
            Description = templateInfo.Description;
            GroupIdentity = templateInfo.GroupIdentity;
            Precedence = templateInfo.Precedence;
            ThirdPartyNotices = templateInfo.ThirdPartyNotices;
            TagsCollection = templateInfo.TagsCollection;
            BaselineInfo = templateInfo.BaselineInfo;
        }

        [JsonConstructor]
        private BlobStorageTemplateInfo(string identity, string name, IEnumerable<string> shortNameList)
        {
            if (string.IsNullOrWhiteSpace(identity))
            {
                throw new ArgumentException($"'{nameof(identity)}' cannot be null or whitespace.", nameof(identity));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            if (!shortNameList.Any())
            {
                throw new ArgumentException($"'{nameof(shortNameList)}' should have at least one entry", nameof(shortNameList));
            }

            Identity = identity;
            Name = name;
            ShortNameList = shortNameList.ToList();
        }

        [JsonProperty]
        //reading manually now to support old format
        public IReadOnlyList<ITemplateParameter> Parameters { get; private set; } = new List<ITemplateParameter>();

        [JsonIgnore]
        string ITemplateInfo.MountPointUri => throw new NotImplementedException();

        [JsonProperty]
        public string? Author { get; private set; }

        [JsonProperty]
        public IReadOnlyList<string> Classifications { get; private set; } = new List<string>();

        [JsonIgnore]
        public string? DefaultName => throw new NotImplementedException();

        [JsonProperty]
        public string? Description { get; private set; }

        [JsonProperty]
        public string Identity { get; private set; }

        [JsonIgnore]
        Guid ITemplateInfo.GeneratorId => throw new NotImplementedException();

        [JsonProperty]
        public string? GroupIdentity { get; private set; }

        [JsonProperty]
        public int Precedence { get; private set; }

        [JsonProperty]
        public string Name { get; private set; }

        [JsonIgnore]
        [Obsolete("Use ShortNameList instead.")]
        string ITemplateInfo.ShortName => throw new NotImplementedException();

        [JsonProperty]
        public IReadOnlyList<string> ShortNameList { get; private set; }

        [JsonIgnore]
        [Obsolete]
        IReadOnlyDictionary<string, ICacheTag> ITemplateInfo.Tags => throw new NotImplementedException();

        [JsonIgnore]
        [Obsolete]
        IReadOnlyDictionary<string, ICacheParameter> ITemplateInfo.CacheParameters => throw new NotImplementedException();

        [JsonIgnore]
        string ITemplateInfo.ConfigPlace => throw new NotImplementedException();

        [JsonIgnore]
        string ITemplateInfo.LocaleConfigPlace => throw new NotImplementedException();

        [JsonIgnore]
        string ITemplateInfo.HostConfigPlace => throw new NotImplementedException();

        [JsonProperty]
        public string? ThirdPartyNotices { get; private set; }

        [JsonProperty]
        public IReadOnlyDictionary<string, IBaselineInfo> BaselineInfo { get; private set; } = new Dictionary<string, IBaselineInfo>();

        [JsonIgnore]
        [Obsolete("This property is deprecated")]
        bool ITemplateInfo.HasScriptRunningPostActions { get; set; }

        [JsonProperty]
        public IReadOnlyDictionary<string, string> TagsCollection { get; private set; } = new Dictionary<string, string>();

        public static BlobStorageTemplateInfo FromJObject(JObject entry)
        {
            string identity = entry.ToString(nameof(Identity)) ?? throw new ArgumentException($"{nameof(entry)} doesn't have {nameof(Identity)} property.", nameof(entry));
            string name = entry.ToString(nameof(Name)) ?? throw new ArgumentException($"{nameof(entry)} doesn't have {nameof(Name)} property.", nameof(entry));
            JToken? shortNameToken = entry.Get<JToken>(nameof(ShortNameList));
            IEnumerable<string> shortNames = shortNameToken.JTokenStringOrArrayToCollection(Array.Empty<string>());
            BlobStorageTemplateInfo info = new BlobStorageTemplateInfo(identity, name, shortNames);
            info.Author = entry.ToString(nameof(Author));
            JArray? classificationsArray = entry.Get<JArray>(nameof(Classifications));
            if (classificationsArray != null)
            {
                List<string> classifications = new List<string>();
                foreach (JToken item in classificationsArray)
                {
                    classifications.Add(item.ToString());
                }
                info.Classifications = classifications;
            }
            info.Description = entry.ToString(nameof(Description));
            info.GroupIdentity = entry.ToString(nameof(GroupIdentity));
            info.Precedence = entry.ToInt32(nameof(Precedence));
            info.ThirdPartyNotices = entry.ToString(nameof(ThirdPartyNotices));

            JObject? baselineJObject = entry.Get<JObject>(nameof(ITemplateInfo.BaselineInfo));
            Dictionary<string, IBaselineInfo> baselineInfo = new Dictionary<string, IBaselineInfo>();
            if (baselineJObject != null)
            {
                foreach (JProperty item in baselineJObject.Properties())
                {
                    IBaselineInfo baseline = new BaselineCacheInfo()
                    {
                        Description = item.Value.ToString(nameof(IBaselineInfo.Description)),
                        DefaultOverrides = item.Value.ToStringDictionary(propertyName: nameof(IBaselineInfo.DefaultOverrides))
                    };
                    baselineInfo.Add(item.Name, baseline);
                }
                info.BaselineInfo = baselineInfo;
            }

            //read parameters
            bool readParameters = false;
            List<ITemplateParameter> templateParameters = new List<ITemplateParameter>();
            JArray? parametersArray = entry.Get<JArray>(nameof(Parameters));
            if (parametersArray != null)
            {
                foreach (JObject item in parametersArray)
                {
                    templateParameters.Add(new BlobTemplateParameter(item));
                }
                readParameters = true;
            }

            JObject? tagsObject = entry.Get<JObject>(nameof(TagsCollection));
            Dictionary<string, string> tags = new Dictionary<string, string>();
            if (tagsObject != null)
            {
                foreach (JProperty item in tagsObject.Properties())
                {
                    tags.Add(item.Name.ToString(), item.Value.ToString());
                }
            }

            //try read tags and parameters - for compatibility reason
            tagsObject = entry.Get<JObject>("tags");
            if (tagsObject != null)
            {
                foreach (JProperty item in tagsObject.Properties())
                {
                    if (item.Value.Type == JTokenType.String)
                    {
                        tags[item.Name.ToString()] = item.Value.ToString();
                    }
                    else if (item.Value is JObject tagObj)
                    {
                        JObject? choicesObject = tagObj.Get<JObject>("ChoicesAndDescriptions");
                        if (choicesObject != null && !readParameters)
                        {
                            Dictionary<string, ParameterChoice> choicesAndDescriptions = new Dictionary<string, ParameterChoice>(StringComparer.OrdinalIgnoreCase);
                            foreach (JProperty cdPair in choicesObject.Properties())
                            {
                                choicesAndDescriptions.Add(cdPair.Name.ToString(), new ParameterChoice(null, cdPair.Value.ToString()));
                            }
                            templateParameters.Add(
                                new BlobTemplateParameter(item.Name.ToString(), "choice")
                                {
                                    Choices = choicesAndDescriptions
                                });
                        }
                        tags[item.Name.ToString()] = tagObj.ToString("defaultValue") ?? "";
                    }
                }
            }
            JObject? cacheParametersObject = entry.Get<JObject>("cacheParameters");
            if (!readParameters && cacheParametersObject != null)
            {
                foreach (JProperty item in cacheParametersObject.Properties())
                {
                    JObject paramObj = (JObject)item.Value;
                    if (paramObj == null)
                    {
                        continue;
                    }
                    string dataType = paramObj.ToString(nameof(BlobTemplateParameter.DataType)) ?? "string";
                    templateParameters.Add(new BlobTemplateParameter(item.Name.ToString(), dataType));
                }
            }

            info.TagsCollection = tags;
            info.Parameters = templateParameters;
            return info;

        }

        private class BaselineCacheInfo : IBaselineInfo
        {
            [JsonProperty]
            public string? Description { get; set; }

            [JsonProperty]
            public IReadOnlyDictionary<string, string> DefaultOverrides { get; set; } = new Dictionary<string, string>();
        }

        private class BlobTemplateParameter : ITemplateParameter
        {
            internal BlobTemplateParameter(ITemplateParameter parameter)
            {
                Name = parameter.Name;
                DataType = parameter.DataType;
                Choices = parameter.Choices;
            }

            internal BlobTemplateParameter(string name, string dataType)
            {
                Name = name;
                DataType = dataType;
            }

            internal BlobTemplateParameter(JObject jObject)
            {
                string? name = jObject.ToString(nameof(Name));
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException($"{nameof(Name)} property should not be null or whitespace", nameof(jObject));
                }

                Name = name!;
                DataType = jObject.ToString(nameof(DataType)) ?? "string";

                if (DataType.Equals("choice", StringComparison.OrdinalIgnoreCase))
                {
                    Dictionary<string, ParameterChoice> choices = new Dictionary<string, ParameterChoice>(StringComparer.OrdinalIgnoreCase);
                    JObject? cdToken = jObject.Get<JObject>(nameof(Choices));
                    if (cdToken != null)
                    {
                        foreach (JProperty cdPair in cdToken.Properties())
                        {
                            choices.Add(
                                cdPair.Name.ToString(),
                                new ParameterChoice(
                                    cdPair.Value.ToString(nameof(ParameterChoice.DisplayName)),
                                    cdPair.Value.ToString(nameof(ParameterChoice.Description))));
                        }
                    }
                    Choices = choices;
                }
            }

            [JsonProperty]
            public string Name { get; internal set; }

            [JsonProperty]
            public string DataType { get; internal set; }

            [JsonProperty]
            public IReadOnlyDictionary<string, ParameterChoice>? Choices { get; internal set; }

            [JsonIgnore]
            public TemplateParameterPriority Priority => throw new NotImplementedException();

            [JsonIgnore]
            string ITemplateParameter.Type => throw new NotImplementedException();

            [JsonIgnore]
            bool ITemplateParameter.IsName => false;

            [JsonIgnore]
            string? ITemplateParameter.DefaultValue => throw new NotImplementedException();

            [JsonIgnore]
            string? ITemplateParameter.DisplayName => throw new NotImplementedException();

            [JsonIgnore]
            string? ITemplateParameter.DefaultIfOptionWithoutValue => throw new NotImplementedException();

            [JsonIgnore]
            string? ITemplateParameter.Description => throw new NotImplementedException();

            [Obsolete]
            [JsonIgnore]
            string? ITemplateParameter.Documentation => throw new NotImplementedException();
        }
    }
}
