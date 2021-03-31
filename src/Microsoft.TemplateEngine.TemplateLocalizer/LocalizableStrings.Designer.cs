﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microsoft.TemplateEngine.TemplateLocalizer {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class LocalizableStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal LocalizableStrings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Microsoft.TemplateEngine.TemplateLocalizer.LocalizableStrings", typeof(LocalizableStrings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Given a template.config file, creates a &quot;localize&quot; directory next to it and exports the localization files into the created directory. If the localization files already exist, the existing translations will be preserved..
        /// </summary>
        internal static string command_export_help_description {
            get {
                return ResourceManager.GetString("command_export_help_description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to If this option is specified, modified files will not be saved to file system and the changes will only be printed to console output..
        /// </summary>
        internal static string command_export_help_dryrun_description {
            get {
                return ResourceManager.GetString("command_export_help_dryrun_description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The list of languages to be supported by this template. The following language list will be used as default if this option is omitted: cs, de, en, es, fr, it, ja, ko, pl, pt-BR, ru, tr, zh-Hans, zh-Hant.
        /// </summary>
        internal static string command_export_help_language_description {
            get {
                return ResourceManager.GetString("command_export_help_language_description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to When specified, subdirectories are also searched for template.json files..
        /// </summary>
        internal static string command_export_help_recursive_description {
            get {
                return ResourceManager.GetString("command_export_help_recursive_description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specifies the path to the template.json file. If a directory is given, template.json file will be searched within the directory. If --recursive options is specified, all the template.json files under the given directory and its subdirectories will be taken as input..
        /// </summary>
        internal static string command_export_help_templatePath_description {
            get {
                return ResourceManager.GetString("command_export_help_templatePath_description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Export command for the following file was cancelled: &quot;{0}&quot;..
        /// </summary>
        internal static string command_export_log_cancelled {
            get {
                return ResourceManager.GetString("command_export_log_cancelled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Execution of export command has completed. {0} files were processed..
        /// </summary>
        internal static string command_export_log_executionEnded {
            get {
                return ResourceManager.GetString("command_export_log_executionEnded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Generating localization files for the following template.json file has failed: &quot;{0}&quot;. Error message: &quot;{1}&quot;..
        /// </summary>
        internal static string command_export_log_templateExportFailedWithError {
            get {
                return ResourceManager.GetString("command_export_log_templateExportFailedWithError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Generating localization files for the following template.json file has failed: &quot;{0}&quot;..
        /// </summary>
        internal static string command_export_log_templateExportFailedWithException {
            get {
                return ResourceManager.GetString("command_export_log_templateExportFailedWithException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Localization files were successfully generated for the template.json file at path &quot;{0}&quot;..
        /// </summary>
        internal static string command_export_log_templateExportSucceeded {
            get {
                return ResourceManager.GetString("command_export_log_templateExportSucceeded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to find &quot;template.json&quot; file under the path &quot;{0}&quot;..
        /// </summary>
        internal static string command_export_log_templateJsonNotFound {
            get {
                return ResourceManager.GetString("command_export_log_templateJsonNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;{0}&quot; command has encountered an error. See the logs for more details..
        /// </summary>
        internal static string generic_log_commandExecutionFailed {
            get {
                return ResourceManager.GetString("generic_log_commandExecutionFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There was an error while running the &quot;{0}&quot; command. Error message: &quot;{1}&quot;. See the logs for more details..
        /// </summary>
        internal static string generic_log_commandExecutionFailedWithErrorMessage {
            get {
                return ResourceManager.GetString("generic_log_commandExecutionFailedWithErrorMessage", resourceCulture);
            }
        }
    }
}
