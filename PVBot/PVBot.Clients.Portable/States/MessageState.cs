﻿using Xamarin.Forms;
using PVBot.Clients.Portable.Controls;
using PVBot.DataObjects.Properties;

namespace PVBot.Clients.Portable.States
{
    public abstract class MessageState
    {
        public void EnterState(MessageCard context, bool isUpdatingState = false)
        {
            if (isUpdatingState)
                UpdateState(context, isUpdatingState);
            else
                SetState(context);
        }

        protected abstract void SetState(MessageCard context);

        protected abstract void UpdateState(MessageCard context, bool isUpdatingState = false);
    }

    public class ChatbotOnlyState : MessageState
    {
        protected override void SetState(MessageCard context)
        {
            // Main container
            context.MainContainer.SetDynamicResource(VisualElement.StyleProperty,
                ResourceKeys.OnlyMessageBotStyle);

            // User Imagge
            Grid.SetRowSpan(context.UserImageContainer, 2);
            Grid.SetColumnSpan(context.UserImageContainer, 2);

            var horizontalOption = LayoutOptions.Start;
            context.UserImageBackground.HorizontalOptions = horizontalOption;
            context.UserImage.HorizontalOptions = horizontalOption;

            var verticalOption = LayoutOptions.Start;
            context.UserImageBackground.VerticalOptions = verticalOption;
            context.UserImage.VerticalOptions = verticalOption;

            // Mesage content
            context.MessageContainer.HorizontalOptions = horizontalOption;

            Grid.SetRow(context.MessageContainer, 1);
            Grid.SetColumn(context.MessageContainer, 1);

            // Mesage Date
            context.DateLabel.HorizontalTextAlignment = TextAlignment.Start;

            Grid.SetRow(context.DateLabel, 2);
            Grid.SetColumn(context.DateLabel, 1);
        }

        protected override void UpdateState(MessageCard context, bool isUpdatingState = false) { }
    }

    public class ChatbotFirtsState : MessageState
    {
        protected override void SetState(MessageCard context)
        {
            // Main container
            context.MainContainer.SetDynamicResource(VisualElement.StyleProperty,
                ResourceKeys.FistMessageBotStyle);

            // User Imagge
            Grid.SetRowSpan(context.UserImageContainer, 2);
            Grid.SetColumnSpan(context.UserImageContainer, 2);

            var horizontalOption = LayoutOptions.Start;
            context.UserImageBackground.HorizontalOptions = horizontalOption;
            context.UserImage.HorizontalOptions = horizontalOption;

            var verticalOption = LayoutOptions.Start;
            context.UserImageBackground.VerticalOptions = verticalOption;
            context.UserImage.VerticalOptions = verticalOption;

            // Mesage content
            context.MessageContainer.HorizontalOptions = horizontalOption;

            Grid.SetRow(context.MessageContainer, 1);
            Grid.SetColumn(context.MessageContainer, 1);

            UpdateState(context);
        }

        protected override void UpdateState(MessageCard context, bool isUpdatingState = false)
        {
            // Mesage Date
            Grid.SetRow(context.DateLabel, 0);
            Grid.SetColumn(context.DateLabel, 0);

            context.DateLabel.IsVisible = false;

            // Main container
            if (isUpdatingState)
                context.MainContainer.SetDynamicResource(VisualElement.StyleProperty,
                    ResourceKeys.FistMessageBotStyle);
        }
    }

    public class ChatbotLastState : MessageState
    {
        protected override void SetState(MessageCard context)
        {
            // Main container
            context.MainContainer.SetDynamicResource(VisualElement.StyleProperty,
                ResourceKeys.LastMessageBotStyle);

            // User Imagge
            context.UserImageBackground.IsVisible = false;
            context.UserImage.IsVisible = false;

            // Mesage content
            context.MessageContainer.HorizontalOptions = LayoutOptions.Start;

            Grid.SetRow(context.MessageContainer, 0);
            Grid.SetColumn(context.MessageContainer, 1);

            UpdateState(context);
        }

        protected override void UpdateState(MessageCard context, bool isUpdatingState = false)
        {
            // Main container
            if (isUpdatingState)
                context.MainContainer.SetDynamicResource(VisualElement.StyleProperty,
                    ResourceKeys.LastMessageBotStyle);

            // Mesage Date
            context.DateLabel.HorizontalTextAlignment = TextAlignment.Start;

            Grid.SetRow(context.DateLabel, 1);
            Grid.SetColumn(context.DateLabel, 1);

        }
    }

    public class ChatbotMiddleState : MessageState
    {
        protected override void SetState(MessageCard context)
        {
            // Mesage content
            context.MessageContainer.HorizontalOptions = LayoutOptions.Start;

            Grid.SetColumn(context.MessageContainer, 1);

            UpdateState(context);
        }

        protected override void UpdateState(MessageCard context, bool isUpdatingState = false)
        {
            // User Imagge
            context.UserImageBackground.IsVisible = false;
            context.UserImage.IsVisible = false;

            // Mesage Date
            Grid.SetRow(context.DateLabel, 0);

            context.DateLabel.IsVisible = false;

            // Main container
            context.MainContainer.SetDynamicResource(VisualElement.StyleProperty,
                ResourceKeys.MiddleMessageBotStyle);
        }
    }
}
