// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.JsonApi.Dom
{
    public interface IDomValidationRules
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IEnumerable<IDomValidationRule<TContext>> DomDocumentValidationRules<TContext>();
        IEnumerable<IDomValidationRule<TContext>> DomResourceValidationRules<TContext>();
        IEnumerable<IDomValidationRule<TContext>> DomRelationshipValidationRules<TContext>();
        IEnumerable<IDomValidationRule<TContext>> DomLinkValidationRules<TContext>();
        #endregion
    }
}