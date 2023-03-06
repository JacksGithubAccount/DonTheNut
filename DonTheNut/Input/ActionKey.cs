using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonTheNut
{
    class ActionKey
    {
        public Keys key;
        public Actions action;
        public InputType type;

        public ActionKey(Keys inputKey, Actions keyAction)
        {
            key = inputKey;
            action = keyAction;
        }
        public ActionKey(ActionKey actionKey)
        {
            key = actionKey.key;
            action = actionKey.action;
            type = InputType.NoInput;
        }
        public ActionKey(ActionKey actionKey, InputType inputType)
        {
            key = actionKey.key;
            action = actionKey.action;
            type = inputType;
        }
        public ActionKey(Keys inputKey, Actions keyAction, InputType inputType)
        {
            key = inputKey;
            action = keyAction;
            type = inputType;
        }
        public void setKeyAction(Keys inputKey, Actions keyAction)
        {
            key = inputKey;
            action = keyAction;
        }
        public void setKey(Keys inputKey)
        {
            key = inputKey;
        }

    }
}
