﻿// --------------------------------------------------------------------------------------------------------------------
// Copyright (c) Lead Pipe Software. All rights reserved.
// Licensed under the MIT License. Please see the LICENSE file in the project root for full license information.
// --------------------------------------------------------------------------------------------------------------------

using LeadPipe.Net.Extensions;
using System.ComponentModel.DataAnnotations;

namespace LeadPipe.Net.Validation
{
    /// <summary>
    /// A custom data validation attribute that ensures string length is no longer than a maximum value.
    /// </summary>
    public class MaximumLengthAttribute : LeadPipeValidationAttribute
    {
        /// <summary>
        /// The Maximum length.
        /// </summary>
        private readonly uint maximumLength;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaximumLengthAttribute"/> class.
        /// </summary>
        /// <param name="maximumLength">The Maximum length.</param>
        /// <param name="ignoreIfConverted">if set to <c>true</c> [ignore if converted].</param>
        public MaximumLengthAttribute(uint maximumLength, bool ignoreIfConverted = false) : base(ignoreIfConverted)
        {
            this.maximumLength = maximumLength;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="MaximumLengthAttribute"/> class from being created.
        /// </summary>
        /// <remarks>
        /// The MaximumLength attribute does not work without a Maximum length value so the default constructor is blocked.
        /// </remarks>
        private MaximumLengthAttribute()
        {
        }

        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">
        /// The value to validate.
        /// </param>
        /// <param name="validationContext">
        /// The context information about the validation operation.
        /// </param>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"/> class.
        /// </returns>
        protected override ValidationResult PerformCustomValidation(object value, ValidationContext validationContext)
        {
            if (value.IsNull())
            {
                return ValidationResult.Success;
            }

            if (validationContext.IsNull())
            {
                validationContext = new ValidationContext(value, null, null) { DisplayName = "The value" };
            }

            var memberNames = new[] { validationContext.MemberName };

            int length = value.ToString().Length;

            if (length > this.maximumLength)
            {
                this.ErrorMessage = string.Format(ValidationMessages.ValueGreaterThanMaximumLength, validationContext.DisplayName, this.maximumLength);

                return new ValidationResult(this.ErrorMessage, memberNames);
            }

            return ValidationResult.Success;
        }
    }
}