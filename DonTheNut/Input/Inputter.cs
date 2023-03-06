using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonTheNut
{
    public enum Actions
    {
        Confirm,
        Cancel,
        Attack,
        Block,
        Run,
        Dodge,
        UseItem,
        Interact,
        OpenMenu,
        Inventory,
        Equipment,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        SwitchMain,
        SwitchOff,
        SwitchItem,
        NoInput
    }
    enum InputType
    {
        NoInput,
        Press,
        Release,
        Hold        
    }
    class Inputter
    {        
        KeyboardState oldState;
        private List<ActionKey> controls;
        public List<ActionKey> actionKeys;

        public Inputter()
        {
            //contains the controls for  player input
            controls = new List<ActionKey>();
            //player input is added to this list which is then checked when an action is called and removed when player is no longer inputting that input
            actionKeys = new List<ActionKey>();
            //instantiate default control scheme
            controls.Add(new ActionKey(Keys.Z, Actions.Confirm));
            controls.Add(new ActionKey(Keys.X, Actions.Cancel));
            controls.Add(new ActionKey(Keys.Z, Actions.Attack));
            controls.Add(new ActionKey(Keys.LeftShift, Actions.Block));
            controls.Add(new ActionKey(Keys.R, Actions.Run));
            controls.Add(new ActionKey(Keys.Space, Actions.Dodge));
            controls.Add(new ActionKey(Keys.C, Actions.UseItem));
            controls.Add(new ActionKey(Keys.A, Actions.Interact));
            controls.Add(new ActionKey(Keys.Escape, Actions.OpenMenu));
            controls.Add(new ActionKey(Keys.I, Actions.Inventory));
            controls.Add(new ActionKey(Keys.O, Actions.Equipment));
            controls.Add(new ActionKey(Keys.Up, Actions.MoveUp));
            controls.Add(new ActionKey(Keys.Down, Actions.MoveDown));
            controls.Add(new ActionKey(Keys.Left, Actions.MoveLeft));
            controls.Add(new ActionKey(Keys.Right, Actions.MoveRight));
            controls.Add(new ActionKey(Keys.S, Actions.SwitchMain));
            controls.Add(new ActionKey(Keys.D, Actions.SwitchOff));
            controls.Add(new ActionKey(Keys.V, Actions.SwitchItem));
        }
        public Actions doesKeyExistinControls(Keys keyToCheck, Actions actionToRemap)
        {
            Actions crossAction = Actions.NoInput;
            if (controls.Exists(x => x.key == keyToCheck))
            {
                List<ActionKey> checkAKey = controls.FindAll(x => x.key == keyToCheck);
                if(checkAKey.Count >= 1)
                {
                    if(checkAKey.Exists(x => x.action == Actions.Confirm) && actionToRemap == Actions.Cancel)
                    {
                        return Actions.Confirm;
                    }
                    else if (checkAKey.Exists(x => x.action == Actions.Cancel) && actionToRemap == Actions.Confirm)
                    {
                        return Actions.Cancel;
                    }
                    else
                    {
                        checkAKey.RemoveAll(x => x.action == Actions.Cancel && x.action == Actions.Confirm);
                        if (checkAKey.Count >= 1)
                        {
                            crossAction = checkAKey[0].action;
                        }
                    }
                }
                return crossAction;
            }
            else
                return actionToRemap;
        }
        public bool isAnyButtonInputTyped(InputType inputType)
        {
            return actionKeys.Exists(x => x.type == inputType);
        }
        public void remap(Keys remappedKey, Actions selectedAction)
        {
            controls.Find(x => x.action == selectedAction).setKey(remappedKey);
        }
        public Keys getKeyforAction(Actions selectedAction)
        {
            return controls.Find(x => x.action == selectedAction).key;
        }
        public bool isActionPressed(Actions selectedAction)
        {
            if (actionKeys.Exists(x => x.action == selectedAction))
            {
                return true;
            }
            else
                return false;
        }
        public bool isActioninputtedbyType(Actions selectedAction, InputType inputType)
        {
            ActionKey actionCheck = new ActionKey(controls.Find(x => x.action == selectedAction));
            actionCheck.type = inputType;
            if(actionKeys.Exists(x => x.action == selectedAction && x.type == inputType))
            {
                    return true;
            }else
                return false;
        }
        public void resetKeystoDefault()
        {
            remap(Keys.Z, Actions.Confirm);
            remap(Keys.X, Actions.Cancel);
            remap(Keys.Z, Actions.Attack);
            remap(Keys.LeftShift, Actions.Block);
            remap(Keys.R, Actions.Run);
            remap(Keys.Space, Actions.Dodge);
            remap(Keys.C, Actions.UseItem);
            remap(Keys.A, Actions.Interact);
            remap(Keys.Escape, Actions.OpenMenu);
            remap(Keys.I, Actions.Inventory);
            remap(Keys.O, Actions.Equipment);
            remap(Keys.Up, Actions.MoveUp);
            remap(Keys.Down, Actions.MoveDown);
            remap(Keys.Left, Actions.MoveLeft);
            remap(Keys.Right, Actions.MoveRight);
        }
        public void PressButton(KeyboardState keyState, Actions action)
        {
            Keys checkKey = controls.Find(x => x.action == action).key;
            ActionKey tempActionKey = new ActionKey(controls.Find(x => x.action == action));
           
            if (keyState.IsKeyDown(checkKey) && oldState.IsKeyDown(checkKey))
            {
                tempActionKey.type = InputType.Hold;
                if (!actionKeys.Exists(x => x.action == action && x.type == InputType.Hold))
                {
                    actionKeys.Add(tempActionKey);
                }
            }
            else
            {
                if (actionKeys.Exists(x => x.action == action && x.type == InputType.Hold))//(actionKeys.Contains(new ActionKey(controls.Find(x => x.action == action), InputType.Hold)))
                {
                    actionKeys.RemoveAll(x => x.action == action && x.type == InputType.Hold);
                }
            }
            if (keyState.IsKeyDown(checkKey) && oldState.IsKeyUp(checkKey))
            {
                tempActionKey.type = InputType.Press;
                if (!actionKeys.Contains(tempActionKey))
                {
                    actionKeys.Add(tempActionKey);
                }
            }
            else
            {
                if (actionKeys.Exists(x => x.action == action && x.type == InputType.Press))
                {
                    actionKeys.RemoveAll(x => x.action == action && x.type == InputType.Press);
                }
            }
            if (keyState.IsKeyUp(checkKey) && oldState.IsKeyDown(checkKey))
            {
                tempActionKey.type = InputType.Release;
                if (!actionKeys.Exists(x => x.action == action && x.type == InputType.Release))
                {
                    actionKeys.Add(tempActionKey);
                }
            }
            else
            {
                if (actionKeys.Exists(x => x.action == action && x.type == InputType.Release))//(actionKeys.Contains(new ActionKey(controls.Find(x => x.action == action), InputType.Hold)))
                {
                    actionKeys.RemoveAll(x => x.action == action && x.type == InputType.Release);
                }
            }
            //if (keyState.IsKeyUp(tempActionKey.key))
            //{
            //    if (actionKeys.Exists(x => x.action == action))//(actionKeys.Contains(controls.Find(x => x.action == action)))
            //        actionKeys.RemoveAll(x => x.action == action);
            //}
        }
        public void update(KeyboardState keyState)
        {          
            for(int c = 0; c < controls.Count; c++)
            {
                PressButton(keyState,  controls[c].action);
            }           

            //if (keyState.GetPressedKeys().Length == 0)
            //{
            //    actionKeys.Clear();
            //}
            oldState = keyState;
        }

    }
}
