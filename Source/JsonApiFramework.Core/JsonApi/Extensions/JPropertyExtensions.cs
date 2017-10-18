// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using Newtonsoft.Json.Linq;

// ReSharper disable once CheckNamespace
namespace JsonApiFramework.JsonApi
{
    /// <summary>Extension methods for the JSON.NET JProperty class.</summary>
    internal static class JPropertyExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Standard predicate if a JProperty name and the parameter name are equal independent of case.</summary>
        public static bool AreNamesEqual(this JProperty jProperty, string name)
        {
            Contract.Requires(jProperty != null);

            var areNamesEqual = String.Compare(name, jProperty.Name, StringComparison.OrdinalIgnoreCase) == 0;
            return areNamesEqual;
        }

        /// <summary>Returns the json:api standard for property names being all lower-case for consistent string compares.</summary>
        public static string GetApiPropertyName(this JProperty jProperty)
        {
            Contract.Requires(jProperty != null);

            var apiPropertyName = jProperty.Name.ToLowerInvariant();
            return apiPropertyName;
        }
        #endregion
    }
}
