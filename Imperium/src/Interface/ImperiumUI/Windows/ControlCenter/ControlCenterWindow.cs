#region

using Imperium.Core;
using Imperium.Interface.Common;
using Imperium.Types;
using Imperium.Util;
using TMPro;
using UnityEngine;

#endregion

namespace Imperium.Interface.ImperiumUI.Windows.ControlCenter;

internal class ControlCenterWindow : ImperiumWindow
{
    private Transform content;

    protected override void InitWindow()
    {
        content = transform.Find("Content");

        InitQuotaAndCredits();
        InitPlayerSettings();
        InitGameSettings();
        InitAnimationSkipping();
    }

    protected override void OnThemeUpdate(ImpTheme themeUpdate)
    {
        ImpThemeManager.Style(
            themeUpdate,
            transform,
            new StyleOverride("Separator", Variant.DARKER)
        );
    }

    private void InitQuotaAndCredits()
    {
        ImpInput.Bind(
            "Left/GroupCredits/Input",
            content,
            Imperium.GameManager.GroupCredits,
            min: 0,
            theme: theme
        );
        ImpInput.Bind(
            "Left/ProfitQuota/Input",
            content,
            Imperium.GameManager.ProfitQuota,
            min: 0,
            theme: theme
        );
        ImpInput.Bind(
            "Left/QuotaDeadline/Input",
            content,
            Imperium.GameManager.QuotaDeadline,
            min: 0,
            max: 3,
            theme: theme,
            interactableInvert: true,
            interactableBindings: Imperium.GameManager.DisableQuota
        );
        ImpButton.Bind(
            "Left/ProfitButtons/FulfillQuota",
            content,
            () => Imperium.GameManager.FulfillQuotaEvent.DispatchToServer(),
            theme: theme
        );
        ImpButton.Bind(
            "Left/ProfitButtons/ResetQuota",
            content,
            () => Imperium.GameManager.ProfitQuota.Reset(),
            theme: theme
        );

        ImpToggle.Bind("Left/DisableQuota", content, Imperium.GameManager.DisableQuota, theme: theme);
    }

    private void InitAnimationSkipping()
    {
        ImpToggle.Bind(
            "Right/AnimationSettings/Scoreboard",
            content,
            Imperium.Settings.AnimationSkipping.Scoreboard,
            theme: theme
        );
        ImpToggle.Bind(
            "Right/AnimationSettings/PlayerSpawn",
            content,
            Imperium.Settings.AnimationSkipping.PlayerSpawn,
            theme: theme
        );
        ImpToggle.Bind(
            "Right/AnimationSettings/InteractHold",
            content,
            Imperium.Settings.AnimationSkipping.InteractHold,
            theme: theme
        );
        ImpToggle.Bind(
            "Right/AnimationSettings/Interact",
            content,
            Imperium.Settings.AnimationSkipping.Interact,
            theme: theme
        );
    }

    private void InitGameSettings()
    {
        ImpToggle.Bind(
            "Left/GameSettings/UnlockShop",
            content,
            Imperium.ShipManager.UnlockShop,
            theme: theme
        );
    }

    private void InitPlayerSettings()
    {
        ImpToggle.Bind("Right/PlayerSettings/GodMode", content, Imperium.Settings.Player.GodMode, theme);
        ImpToggle.Bind("Right/PlayerSettings/Muted", content, Imperium.Settings.Player.Muted, theme);

        ImpToggle.Bind(
            "Right/PlayerSettings/InfiniteSprint",
            content,
            Imperium.Settings.Player.InfiniteSprint,
            theme
        );

        ImpToggle.Bind(
            "Right/PlayerSettings/Invisibility",
            content,
            Imperium.Settings.Player.Invisibility, theme
        );

        ImpToggle.Bind(
            "Right/PlayerSettings/DisableLocking",
            content,
            Imperium.Settings.Player.DisableLocking,
            theme
        );

        ImpToggle.Bind(
            "Right/PlayerSettings/InfiniteBattery",
            content,
            Imperium.Settings.Player.InfiniteBattery,
            theme
        );

        ImpToggle.Bind(
            "Right/PlayerSettings/PickupOverwrite",
            content,
            Imperium.Settings.Player.PickupOverwrite,
            theme
        );

        ImpToggle.Bind(
            "Right/PlayerSettings/DisableOOB",
            content,
            Imperium.Settings.Player.DisableOOB,
            theme
        );

        ImpToggle.Bind(
            "Right/PlayerSettings/EnableFlying",
            content,
            Imperium.Settings.Player.EnableFlying,
            theme
        );

        ImpToggle.Bind(
            "Right/PlayerSettings/Permadrunk",
            content,
            Imperium.Settings.Player.Permadrunk,
            theme
        );

        ImpToggle.Bind(
            "Right/PlayerSettings/FlyingNoClip",
            content,
            Imperium.Settings.Player.FlyingNoClip,
            theme,
            interactableBindings: Imperium.Settings.Player.EnableFlying
        );

        ImpSlider.Bind(
            path: "Right/FieldOfView",
            container: content,
            valueBinding: Imperium.Settings.Player.CustomFieldOfView,
            indicatorDefaultValue: ImpConstants.DefaultFOV,
            indicatorUnit: "°",
            theme: theme
        );

        ImpSlider.Bind(
            path: "Right/MovementSpeed",
            container: content,
            theme: theme,
            valueBinding: Imperium.Settings.Player.MovementSpeed
        );

        ImpSlider.Bind(
            path: "Right/JumpForce",
            container: content,
            theme: theme,
            valueBinding: Imperium.Settings.Player.JumpForce
        );

        ImpSlider.Bind(
            path: "Right/NightVision",
            container: content,
            theme: theme,
            valueBinding: Imperium.Settings.Player.NightVision,
            indicatorUnit: "%"
        );

        ImpSlider.Bind(
            path: "Right/PushForce",
            container: content,
            theme: theme,
            debounceTime: 0.1f,
            valueBinding: Imperium.PlayerManager.CarPushForceBinding
        );
    }
}