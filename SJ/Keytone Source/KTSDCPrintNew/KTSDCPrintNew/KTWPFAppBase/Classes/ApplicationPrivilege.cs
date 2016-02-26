using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using System.Data;

namespace KTWPFAppBase
{
    public class ApplicationPrivilege
    {
        public ApplicationPrivilege()
        {

        }

        /// <summary>
        /// This method is used to iterate throught all the controls in the provided visual.
        /// This is used to apply application privileges against the role.
        /// </summary>
        /// <param name="myVisual">Control on which operation has to be perform. It can be Window/UserControl/Any Container Control.</param>
        /// <param name="accessibleMenus">List of accessible menus.</param>
        /// <param name="visibility">If true then disable control else hide the control.</param>
        public static void EnumVisual(Visual myVisual, List<string> accessibleMenus, bool visibility)
        {
            string Header = "ApplicationPrivilege::EnumVisual: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(myVisual); i++)
                {
                    // Retrieve child visual at specified index value.
                    Visual childVisual = (Visual)VisualTreeHelper.GetChild(myVisual, i);
                    if (childVisual is Button)
                    {
                        if (((Button)childVisual).Tag != null)
                        {
                            if (accessibleMenus.Contains(((Button)childVisual).Tag.ToString()))
                            {
                                if (visibility)
                                    ((Button)childVisual).IsEnabled = true;
                                else
                                    ((Button)childVisual).Visibility = Visibility.Visible;
                            }
                            else
                            {
                                if (visibility)
                                    ((Button)childVisual).IsEnabled = false;
                                else
                                    ((Button)childVisual).Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                    else if (childVisual is MenuItem)
                    {
                        if (((MenuItem)childVisual).Tag != null)
                        {
                            if (accessibleMenus.Contains(((MenuItem)childVisual).Tag.ToString()))
                            {
                                ((MenuItem)childVisual).IsEnabled = true;
                            }
                            else
                            {
                                if (visibility)
                                    ((MenuItem)childVisual).IsEnabled = false;
                                else
                                    ((MenuItem)childVisual).Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                    else if (childVisual is Expander)
                    {
                        if (((Expander)childVisual).Tag != null)
                        {
                            if (accessibleMenus.Contains(((Expander)childVisual).Tag.ToString()))
                            {
                                ((Expander)childVisual).IsEnabled = true;
                            }
                            else
                            {
                                if (visibility)
                                    ((Expander)childVisual).IsEnabled = false;
                                else
                                    ((Expander)childVisual).Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                    else if (childVisual is StackPanel)
                    {
                        if (((StackPanel)childVisual).Tag != null)
                        {
                            if (accessibleMenus.Contains(((StackPanel)childVisual).Tag.ToString()))
                            {
                                ((StackPanel)childVisual).IsEnabled = true;
                            }
                            else
                            {
                                if (visibility)
                                    ((StackPanel)childVisual).IsEnabled = false;
                                else
                                    ((StackPanel)childVisual).Visibility = Visibility.Collapsed;
                            }
                        }
                    }

                    // Enumerate children of the child visual object.

                    if (childVisual is MenuItem && ((MenuItem)childVisual).Items.Count > 0)
                        EnableSubMenuItems((MenuItem)childVisual, accessibleMenus, visibility);
                    else
                        EnumVisual(childVisual, accessibleMenus, visibility);
                }
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        private static void EnableSubMenuItems(MenuItem parentMenu, List<string> accessibleMenus, bool visibility)
        {
            string Header = "ApplicationPrivilege::EnableSubMenuItems: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                if (parentMenu.Items.Count > 0)
                {
                    foreach (MenuItem childItem in parentMenu.Items)
                    {
                        if (childItem.Tag != null)
                        {
                            if (accessibleMenus.Contains(childItem.Tag.ToString()))
                            {
                                childItem.IsEnabled = true;
                            }
                            else
                            {
                                if (visibility)
                                    childItem.IsEnabled = false;
                                else
                                    childItem.Visibility = Visibility.Collapsed;
                            }

                            if (childItem.Items.Count > 0)
                                EnableSubMenuItems(childItem, accessibleMenus, visibility);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }
    }
}
