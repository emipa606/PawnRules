using System;
using PawnRules.Data;
using PawnRules.Patch;
using UnityEngine;
using Verse;

namespace PawnRules.Interface;

internal class Dialog_Alert : Window
{
    public enum Buttons
    {
        Ok,
        OkCancel,
        YesNo
    }

    private readonly Buttons _buttons;
    private readonly string _message;
    private readonly Action _onAccept;
    private readonly Action _onCancel;
    private bool _isAccepted;

    private Dialog_Alert(string message, Buttons buttons = Buttons.Ok, Action onAccept = null, Action onCancel = null)
    {
        doCloseButton = false;
        closeOnAccept = true;
        closeOnClickedOutside = true;
        absorbInputAroundWindow = true;

        _message = message;
        _buttons = buttons;
        _onAccept = onAccept;
        _onCancel = onCancel;

        var wrap = Text.WordWrap;
        Text.WordWrap = true;
        InitialSize = new Vector2(400f, 72f + Text.CalcHeight(_message, 364f));
        Text.WordWrap = wrap;
    }

    public override Vector2 InitialSize { get; }

    public static void Open(string message, Buttons buttons = Buttons.Ok, Action onAccept = null,
        Action onCancel = null)
    {
        Find.WindowStack.Add(new Dialog_Alert(message, buttons, onAccept, onCancel));
    }

    public override void DoWindowContents(Rect rect)
    {
        var buttonHeight = 30f;
        var buttonWidth = 100f;
        var gap = 8f;
        var buttonRowY = rect.height - buttonHeight;
        var messageRect = new Rect(0f, 0f, rect.width, buttonRowY - gap);

        var wrap = Text.WordWrap;
        var anchor = Text.Anchor;
        Text.WordWrap = true;
        Text.Anchor = TextAnchor.UpperLeft;
        Widgets.Label(messageRect, _message);
        Text.Anchor = anchor;
        Text.WordWrap = wrap;

        if (_buttons == Buttons.Ok)
        {
            var okRect = new Rect((rect.width - buttonWidth) / 2f, buttonRowY, buttonWidth, buttonHeight);
            if (Widgets.ButtonText(okRect, Lang.Get("Button.OK")))
            {
                _isAccepted = true;
                _onAccept?.Invoke();
                Close();
            }

            return;
        }

        var leftButton = new Rect((rect.width - (buttonWidth * 2f) - gap) / 2f, buttonRowY, buttonWidth, buttonHeight);
        var rightButton = new Rect(leftButton.xMax + gap, buttonRowY, buttonWidth, buttonHeight);

        if (Widgets.ButtonText(leftButton, _buttons == Buttons.YesNo ? Lang.Get("Button.Yes") : Lang.Get("Button.OK")))
        {
            _isAccepted = true;
            _onAccept?.Invoke();
            Close();
        }

        if (Widgets.ButtonText(rightButton, _buttons == Buttons.YesNo ? Lang.Get("Button.No") : Lang.Get("Button.Cancel")))
        {
            Close();
        }
    }

    public override void Close(bool doCloseSound = true)
    {
        if (!_isAccepted)
        {
            _onCancel?.Invoke();
        }

        base.Close(doCloseSound);
    }
}