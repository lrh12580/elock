   M S B A M L   `   `   `  ���� M  IWPFVisifire.Charts, Version=5.1.7.0, Culture=neutral, PublicKeyToken=nullM9clr-namespace:Visifire.Charts;assembly=WPFVisifire.ChartsVisifire.Charts   DesktopApplication#   DesktopApplication.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Hvc9clr-namespace:Visifire.Charts;assembly=WPFVisifire.Charts     -     � Title$  E-Lock��$	��650��$
��1025�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=� � 	Resources    SigninButtonStyle �� Property �� Value �� 
TargetType ��Name	 J� SnapsToDevicePixels
 �� Triggers P� Property P� Value  
exitButton  BoxRadioButton �� 
TargetType �� CornerRadius �� Triggers��%�  &	        &	 �     &	 �    �� ���  � ��$ 16(���  � ��$
 Bold"���  � ��
 �������  � �� �� " ��.R��� $ Chrome��#��z��	 . ._��� #��z��#��z����. #	 z	 #��z��
 P�  �@J�IsMouseOver . . ���  � ��
 � d ��� .���  � �� �� " ��.R��� $ Chrome��
�������#��z��	 . ._��� #��z��#��z����. #	 z	 #��z��
 P�  � �� . . ���  � ��
 � � ��� " 	�.���  � ��$ 14(���  � ��
 �������  � ��$
 Bold"���  � ��
 �������  �@J�SnapsToDevicePixels . ��  � ��$ Center=���  � ��$ Center=���  � �� �� " 	�.R��� $ Chrome��
�������$��1q�$ 	10,0,10,0��#��z��	 . ._��� #��z��#��z����. #	 z	 #��z�� P�  �@d�	IsChecked$
 trueY�. ���  � ��
 �������  � ��$
 Bold"���  � ��
 �����P�  �@J�IsMouseOver . . ���  � ��
 ���������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FFDF4852�� � Offset$ 0Q�� $ 	#FF3A3847��$ 1Q�.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* �.+�� -   $��30��$��0��
�������.+���  �� Orientation$ 
Horizontal=�$��	15,10,5,5q�.+��� $��Icon/E-lock.png��$
��Left=��� -   $��txtTitle��$��5,0,0,0q�$��E-lock��$��Center=�$��14(�$��微软雅黑)�
��������� $ 
Horizontal=�$��Right=�$��0,5,15,0q�.+��� -    ��Name$ btn_min��$��Right=�$��18��$��20��$��0q�#��[ ����  �� ImageSource$ Icon/minisizeIcon.png���� -   $ btn_max��$��Right=�$��20��$��20��$��10,0,0,0q�$��0q�#��[ ���� $ Icon/exitIcon.png��� $��1��$��Stretch=� � ColumnDefinitions �� ���� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFDEDEDE��$ 1Q�� $
 Gray��.+��  � Orientation$ 
Horizontal=�$��Center=�$	��120��$��452,0,452.8,20q�.+�	� -    	�Name$ 
HomeButton��$
��Home��$��0,5,0,5q�$��30��$��60��#��[  d� 	IsChecked$
 TrueY�	� -   $��History��$��0,5,0,5q�$��30��$��60��#��[ � -   $��
stackpanel��$��0��$ 
Horizontal=�$��Right=�$��0,10,0,0q�.+��� -	   $��SiginButton��$��	未登录��$��80��$��5,0,5,0q�#��[  ��Z� �� 
�������	 �� X1$ 0��	 �� X2$ 0��	 �� Y1$ 25��$��0,0,15,0q�$��2���� -
   $ SettingButton��$��5,0,20,0q�$��20��$��20��$��0q�#��[   � ContextMenu �� -     ��Name$  contextMenu��.@�m� $��帮助及反馈��m� -   $��退出������ $ /Icon/settingIcon.png�� � -   $��myFrame��$��2��!  � Source$! PageMain.xaml?�"  � NavigationUIVisibility$" Hidden=�
�������