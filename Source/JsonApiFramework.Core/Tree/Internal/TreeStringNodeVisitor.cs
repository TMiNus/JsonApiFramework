﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace JsonApiFramework.Tree.Internal
{
    /// <summary>
    /// Represents a node visitor that builds a string of the object tree by
    /// visiting the nodes in document order.
    /// </summary>
    internal class TreeStringNodeVisitor : NodeVisitor
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public TreeStringNodeVisitor()
        {
            this.StringBuilder = new StringBuilder();
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        public string TreeString => this.StringBuilder.ToString().TrimEnd();

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region NodeVisitor Overrides
        public override VisitResult Visit(Node node, int depth)
        {
            Contract.Requires(node != null);
            Contract.Requires(depth >= 0);

            this.AddNodeToTreeString(node, depth);

            return VisitResult.ContinueWithChildAndSiblingNodes;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private StringBuilder StringBuilder { get; }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region TreeStringNodeVisitor Overrides
        protected virtual bool ShouldIndent(Node node)
        { return true; }

        protected virtual bool ShouldAppendLine(Node node)
        { return true; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void AddNodeToTreeString(Node node, int depth)
        {
            Contract.Requires(node != null);
            Contract.Requires(depth >= 0);

            this.AddIndentToTreeString(node, depth);
            this.AddNodeDescriptionToTreeString(node);
        }

        private void AddIndentToTreeString(Node node, int depth)
        {
            Contract.Requires(node != null);
            Contract.Requires(depth >= 0);

            if (!this.ShouldIndent(node))
                return;

            var indentSpace = depth * IndentSize;
            this.StringBuilder.Append(WhitespaceAsCharacter, indentSpace);
        }

        private void AddNodeDescriptionToTreeString(Node node)
        {
            Contract.Requires(node != null);

            var nodeDescription = CreateNodeDescription(node);
            this.StringBuilder.Append(nodeDescription);

            if (this.ShouldAppendLine(node))
            {
                this.StringBuilder.AppendLine();
            }
        }

        private static string CreateNodeDescription(Node node)
        {
            Contract.Requires(node != null);

            var nodeHasAttributes = node.HasAttributes();
            if (nodeHasAttributes)
            {
                var attributesAsStrings = String.Join(WhitespaceAsString, node.Attributes());
                var nodeDescriptionWithAttributes = "<{0} {1}>".FormatWith(node.Name, attributesAsStrings);
                return nodeDescriptionWithAttributes;
            }

            var nodeDescription = "<{0}>".FormatWith(node.Name);
            return nodeDescription;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const char WhitespaceAsCharacter = ' ';
        private const string WhitespaceAsString = " ";
        private const int IndentSize = 2;
        #endregion
    }
}
