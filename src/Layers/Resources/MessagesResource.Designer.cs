﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class MessagesResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MessagesResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.MessagesResource", typeof(MessagesResource).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All combinations used.
        /// </summary>
        public static string AllCombinationsUsed {
            get {
                return ResourceManager.GetString("AllCombinationsUsed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The code contains invalid characters!.
        /// </summary>
        public static string CodeInvalid {
            get {
                return ResourceManager.GetString("CodeInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Custom code should not be longer than {0}.
        /// </summary>
        public static string CodeLengthExceed {
            get {
                return ResourceManager.GetString("CodeLengthExceed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The code is not exist for this url!.
        /// </summary>
        public static string CodeNotExist {
            get {
                return ResourceManager.GetString("CodeNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Code is used!.
        /// </summary>
        public static string CodeUsed {
            get {
                return ResourceManager.GetString("CodeUsed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Something went wrong!.
        /// </summary>
        public static string UnKnownError {
            get {
                return ResourceManager.GetString("UnKnownError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The URL is exist!.
        /// </summary>
        public static string UrlExist {
            get {
                return ResourceManager.GetString("UrlExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The URL is not valid!.
        /// </summary>
        public static string UrlInvalid {
            get {
                return ResourceManager.GetString("UrlInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is no url associated with the code!.
        /// </summary>
        public static string UrlUnassociated {
            get {
                return ResourceManager.GetString("UrlUnassociated", resourceCulture);
            }
        }
    }
}
