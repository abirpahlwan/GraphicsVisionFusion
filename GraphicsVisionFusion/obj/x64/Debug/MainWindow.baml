   M S B A M L   `   `   `  ����O  KGraphicsVisionFusion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:GraphicsVisionFusionGraphicsVisionFusion  5   	   T PEmgu.CV.UI, Version=3.1.0.2504, Culture=neutral, PublicKeyToken=7281126722ab4438;,clr-namespace:Emgu.CV.UI;assembly=Emgu.CV.UI
Emgu.CV.UI 5   	   R NSharpGL.WPF, Version=2.4.0.0, Culture=neutral, PublicKeyToken=27fc851303210b27>.clr-namespace:SharpGL.WPF;assembly=SharpGL.WPFSharpGL.WPF 5   	   %    GraphicsVisionFusion.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   4local"clr-namespace:GraphicsVisionFusion     5   	   =emgu,clr-namespace:Emgu.CV.UI;assembly=Emgu.CV.UI     5   	   BsharpGL.clr-namespace:SharpGL.WPF;assembly=SharpGL.WPF     5   	   U QMaterialDesignThemes.Wpf, Version=2.2.0.725, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   X	 TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit 	 5   	   -   5        $��Roboto)�5	   	     � Title$  Graphics Vision Fusion��5
   	   $	��720��6(   $
��1366��65   ���	#FF252525.��5      � .+�5   
   � $
��Left=�6   $��25q�6*   $	��Top=�66   $	��640��6N   $	��480��6Z   .+�5      7  1System.Windows.Forms.Integration.WindowsFormsHost  $	��Top=�6   $	��480��67   $	��640��6D      
Background
 �����   Child. 5        Emgu.CV.UI.ImageBox  -   5        
imgBox��5   2   5      5      � 5   
   $
��Left=�6   $��
25,505,0,0q�6*   $��Stretch=�6>   $	��640��6Z   .+�5      �� -   5         ��Name$ col1up��5      $
��Left=�6%   $��	25,80,0,0q�6@   $	��Top=�6S    � Maximum$	 180Q�6k   $	��250��6y    � Value$ 75Q�6�   �� 5      -   5        $ col1low��5      $
��Left=�6&   $��
25,120,0,0q�6A   $	��Top=�6U   $	 180Q�6m   $	��250��6{   $ 65Q�6�   �� 5       -   5        $ col2up��5       $
��Left=�6%   $��
325,80,0,0q�6@   $	��Top=�6T   $	 180Q�6l   $	��250��6z   �� 5!      -   5        $ col2low��5!      $
��Left=�6&   $��325,120,0,0q�6A   $	��Top=�6V   $	 180Q�6n   $	��250��6|   5"      � 5$   
   $��Right=�6   $��25q�6+   $	��Top=�67   $	��640��6O   $	��480��6[   .+�5%        SharpGL.WPF.OpenGLControl  -   5          Name$ openGLControl��5%   $      DrawFPS .     RenderContextType$	 FBO=�5'      	   	FrameRate$	 12Q�65   
�������6D   5(      � 5*   
   $��Right=�6   $��
0,510,25,0q�6+   $	��Top=�6?   $	��640��6W   .+�5+      # 	 Xceed.Wpf.Toolkit.ColorPicker  -   5        $��colorPicker��5+      $	��100��62   $��25��6>   
���   �$��Light"�6]   
   ShowDropDownButton
 .     ShowStandardColors .     ShowAdvancedButton .     ShowAvailableColors .    ShowRecentColors .    AvailableColorsHeader$ 
Pick Color��5.         RecentColorsHeader$ Recent Colors��6B   !   AvailableColorsSortingMode$ HueSaturationBrightness=�5/      50      51      � 53   
   $��Right=�6   $��
0,580,25,0q�6+   $	��Top=�6?   $	��640��6W   .+�54      �� -	   5        $��145,10,395,0q�54      $	��100��6+   $��40��67   
����������	#FF03A9F4$��
ExtraLight"�6�   $��18(�6�   .��6�   Rotate6�   �� 55      -
   5        $��395,10,145,0q�55      $	��100��6+   $��40��67   
����������	#FF03A9F4$��
ExtraLight"�6�   $��18(�6�   .��6�   Clear6�   56      58      59      