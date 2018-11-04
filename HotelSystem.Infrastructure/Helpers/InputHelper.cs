using System;
using System.Linq;
using System.Windows.Input;

namespace HotelSystem.Infrastructure.Helpers
{
   [Flags]
   public enum InputConfiguration
   {
      Numeric = 0x1,  // Number and NumPad numeric keys
      CustomTextBox = 0x2,  // DecimalTextBox/IntegerTextBox keys
      GridNavigation = 0x4   // Grid navigation keys
                             // next flags will be 0x8, 0x10, 0x20, 0x40, ...
   }

   public static class InputHelper
   {
      #region Key Groupings

      /// <summary>
      /// Numeric keys on the keyboard.
      /// </summary>
      private static readonly Key[] _numericKeys =
      {
            Key.D0,
            Key.D1,
            Key.D2,
            Key.D3,
            Key.D4,
            Key.D5,
            Key.D6,
            Key.D7,
            Key.D8,
            Key.D9,
            Key.NumPad0,
            Key.NumPad1,
            Key.NumPad2,
            Key.NumPad3,
            Key.NumPad4,
            Key.NumPad5,
            Key.NumPad6,
            Key.NumPad7,
            Key.NumPad8,
            Key.NumPad9
        };

      /// <summary>
      /// Non-numeric keys that have functionality in custom textboxes
      /// </summary>
      private static readonly Key[] _customTextBoxHandledKeys =
      {
            Key.Back,
            Key.Delete,
            Key.Subtract,
            Key.OemMinus
        };

      /// <summary>
      /// Keys which will not be actioned in custom textboxes but will be 
      /// allowed to bubble up the WPF visual tree. 
      /// Note: Key.System allows Alt + F4 functionality.
      /// </summary>
      private static readonly Key[] _customTextBoxUnhandledKeys =
      {
            Key.Enter,
            Key.Tab,
            Key.LeftShift,
            Key.RightShift,
            Key.System
        };

      /// <summary>
      /// Keys which will not be actioned in custom datagrid but will be 
      /// allowed to bubble up the WPF visual tree. e.g. navigation keys
      /// </summary>
      private static readonly Key[] _gridNavigationKeys =
      {
            Key.Enter,
            Key.Tab,
            Key.LeftShift,
            Key.RightShift,
            Key.LeftCtrl,
            Key.RightCtrl,
            Key.System,
            Key.PageUp,
            Key.PageDown,
            Key.Left,
            Key.Right,
            Key.Up,
            Key.Down
        };

      #endregion

      /// <summary>
      /// Determines if the specified key is numeric.
      /// </summary>
      /// <param name="key">Key entered</param>
      public static bool IsKeyNumeric(Key key)
      {
         return _numericKeys.Contains(key);
      }

      /// <summary>
      /// Determines if the specified key is ignored
      /// </summary>
      /// <param name="key">Key entered</param>
      public static bool IsKeyIgnored(Key key, InputConfiguration flags)
      {
         bool result = false;

         if ((flags & InputConfiguration.Numeric) == InputConfiguration.Numeric)
         {
            result = !_numericKeys.Contains(key);
         }

         if ((flags & InputConfiguration.CustomTextBox) == InputConfiguration.CustomTextBox)
         {
            result = result &&
                     !_customTextBoxHandledKeys.Contains(key) &&
                     !_customTextBoxUnhandledKeys.Contains(key);
         }

         if ((flags & InputConfiguration.GridNavigation) == InputConfiguration.GridNavigation)
         {
            result = result &&
                     !_gridNavigationKeys.Contains(key);
         }

         return result;
      }

      /// <summary>
      /// Determines if the specified key is handled
      /// </summary>
      /// <param name="key"></param>
      /// <param name="flags"></param>
      /// <returns></returns>
      public static bool IsKeyHandled(Key key, InputConfiguration flags)
      {
         bool result = false;

         if ((flags & InputConfiguration.Numeric) == InputConfiguration.Numeric)
         {
            result = _numericKeys.Contains(key);
         }

         if ((flags & InputConfiguration.CustomTextBox) == InputConfiguration.CustomTextBox)
         {
            result = result ||
                     _customTextBoxHandledKeys.Contains(key);
         }

         if ((flags & InputConfiguration.GridNavigation) == InputConfiguration.GridNavigation)
         {
            result = result ||
                     _gridNavigationKeys.Contains(key);
         }

         return result;
      }

      /// <summary>
      /// Translates a numeric keypress into a decimal.
      /// </summary>
      /// <param name="key">Key entered</param>
      public static decimal GetDecimalFromKey(Key key)
      {
         decimal output = 0M;

         switch (key)
         {
            case Key.D0:
            case Key.NumPad0:
               break;
            case Key.D1:
            case Key.NumPad1:
               {
                  output = 1M;
                  break;
               }
            case Key.D2:
            case Key.NumPad2:
               {
                  output = 2M;
                  break;
               }
            case Key.D3:
            case Key.NumPad3:
               {
                  output = 3M;
                  break;
               }
            case Key.D4:
            case Key.NumPad4:
               {
                  output = 4M;
                  break;
               }
            case Key.D5:
            case Key.NumPad5:
               {
                  output = 5M;
                  break;
               }
            case Key.D6:
            case Key.NumPad6:
               {
                  output = 6M;
                  break;
               }
            case Key.D7:
            case Key.NumPad7:
               {
                  output = 7M;
                  break;
               }
            case Key.D8:
            case Key.NumPad8:
               {
                  output = 8M;
                  break;
               }
            case Key.D9:
            case Key.NumPad9:
               {
                  output = 9M;
                  break;
               }
            default:
               throw new ArgumentOutOfRangeException("Invalid key: " +
                   key.ToString());
         }

         return output;
      }

      /// <summary>
      /// Translates a numeric keypress into an Integer.
      /// </summary>
      /// <param name="key">Key entered</param>
      public static int GetIntegerFromKey(Key key)
      {
         int output = 0;

         switch (key)
         {
            case Key.D0:
            case Key.NumPad0:
               break;
            case Key.D1:
            case Key.NumPad1:
               {
                  output = 1;
                  break;
               }
            case Key.D2:
            case Key.NumPad2:
               {
                  output = 2;
                  break;
               }
            case Key.D3:
            case Key.NumPad3:
               {
                  output = 3;
                  break;
               }
            case Key.D4:
            case Key.NumPad4:
               {
                  output = 4;
                  break;
               }
            case Key.D5:
            case Key.NumPad5:
               {
                  output = 5;
                  break;
               }
            case Key.D6:
            case Key.NumPad6:
               {
                  output = 6;
                  break;
               }
            case Key.D7:
            case Key.NumPad7:
               {
                  output = 7;
                  break;
               }
            case Key.D8:
            case Key.NumPad8:
               {
                  output = 8;
                  break;
               }
            case Key.D9:
            case Key.NumPad9:
               {
                  output = 9;
                  break;
               }
            default:
               throw new ArgumentOutOfRangeException("Invalid key: " +
                   key.ToString());
         }

         return output;
      }
   }
}
