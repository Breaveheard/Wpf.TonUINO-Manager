﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Wpf.TonUINOManager.Themes.Controls
{
    public class NumberBoxFloat : TextBox
    {
        public const string FloatRegex = "^-?\\d*\\.?\\d*$";

        public static DependencyPropertyKey IsValidPropertyKey = DependencyProperty.RegisterReadOnly("IsValid", typeof(bool), typeof(NumberBoxFloat), new PropertyMetadata(true));

        public static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(float), typeof(NumberBoxFloat));
        public static DependencyProperty NumberSignProperty = DependencyProperty.Register("NumberSign", typeof(NumberSign), typeof(NumberBoxFloat), new PropertyMetadata(NumberSign.Both));
        public static DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(float), typeof(NumberBoxFloat), new PropertyMetadata(float.MinValue));
        public static DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(float), typeof(NumberBoxFloat), new PropertyMetadata(float.MaxValue));

        public bool IsValid
        {
            get => (bool)GetValue(IsValidPropertyKey.DependencyProperty);
            private set => SetValue(IsValidPropertyKey, value);
        }

        public float Value
        {
            get => (float)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public NumberSign NumberSign
        {
            get => (NumberSign)GetValue(NumberSignProperty);
            set => SetValue(NumberSignProperty, value);
        }

        public float Minimum
        {
            get => (float)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public float Maximum
        {
            get => (float)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        private string _lastText;
        private float _lastValidValue;

        static NumberBoxFloat()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumberBoxFloat), new FrameworkPropertyMetadata(typeof(NumberBoxFloat)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (NumberSign == NumberSign.Positive && (Maximum < 0 || Minimum < 0))
                throw new ArgumentOutOfRangeException("Minimum or Maximum value cannot be negative because the NumberSign is positive.");
            if (NumberSign == NumberSign.Negative && (Maximum > 0 || Minimum > 0))
                throw new ArgumentOutOfRangeException("Minimum or Maximum value cannot be positive because the NumberSign is negative.");
            if (Minimum > Maximum)
                throw new ArgumentException("Minimum value cannot be greater than Maximum value.");

            CheckValue();
        }

        private void CheckValue()
        {
            if ((NumberSign == NumberSign.Positive && Value >= 0) || (NumberSign == NumberSign.Negative && Value <= 0) || NumberSign == NumberSign.Both)
            {
                if (Value <= Minimum)
                    Value = Minimum;
                else if (Value >= Maximum)
                    Value = Maximum;
            }
            else
            {
                if (NumberSign == NumberSign.Positive)
                    Value = Minimum;
                else if (NumberSign == NumberSign.Negative)
                    Value = Maximum;
            }

            Text = Value.ToString();
            _lastValidValue = Value;
        }

        private void CheckText()
        {
            if (string.IsNullOrWhiteSpace(Text) || Text == "-" || Text == ".")
                Value = Minimum;
            else if (Regex.IsMatch(Text, FloatRegex))
            {
                if (float.TryParse(Text, out float value) && ((NumberSign == NumberSign.Positive && value >= 0) || (NumberSign == NumberSign.Negative && value <= 0) || NumberSign == NumberSign.Both))
                {
                    if (value <= Minimum)
                        Value = Minimum;
                    else if (value >= Maximum)
                        Value = Maximum;
                    else
                        Value = value;
                }
                else // case where float is too large or too small
                {
                    if (Text.StartsWith("-"))
                        Value = Minimum;
                    else
                        Value = Maximum;
                }
            }
            else
                Value = _lastValidValue;

            Text = Value.ToString();
            _lastValidValue = Value;
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            if (!Regex.IsMatch(Text, FloatRegex))
            {
                Text = _lastText;
                CaretIndex = e.Changes.Last().Offset;
            }
            else
            {
                IsValid = float.TryParse(Text, out float value) && ((NumberSign == NumberSign.Positive && value >= 0) || (NumberSign == NumberSign.Negative && value <= 0) || NumberSign == NumberSign.Both) && Minimum <= value && value <= Maximum;
                if (IsValid)
                    Value = value;
            }

            _lastText = Text;
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            CheckText();
            base.OnLostFocus(e);
        }

    }
}
