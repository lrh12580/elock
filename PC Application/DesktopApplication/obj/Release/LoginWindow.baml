   M S B A M L   `   `   `  ����   DesktopApplication$    DesktopApplication.LoginWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  登录��$	��300��$	��400�� � WindowStartupLocation$ CenterScreen=� � Icon$ Icon/E-lock.png�� � 	Resources    ButtonStyle �� Property �� Value �� 
TargetType ��Name J� SnapsToDevicePixels	 �� Triggers
 P� Property P� Value��%�   &	        �� .���  � �� �� " ��.R��� $ Chrome��#��z��#��z�� . ._��� #��z��#��z����. # z #��z��	 P� 
 � �� . . ���  � ��$
 Bold"���  � ��
 � � ���  � ��
 ����.��� $��15,20,15,30q� � RowDefinitions �� $
��Auto ��� $��* ��� $��* ��� $��* �.+��� $��Icon/icon_key.png��$��80���� $��2q�
�������$��
39,5,40,10q�$��1��._�� -   $��AccountPanel�� � ColumnDefinitions �� $��* ��� $��5* �.+��� $��账号��$��15(�$��Center=�$��0��$��Center=��� -   $��AccountTextBox��$��10,0,0,0q�$��1��$��0q�$��Center=��� $��2q�
�������$��
39,5,40,10q�$��2��._�� -   $��KeyPanel�� �� $��* ��� $��5* �.+��� $��密码��$��15(�$��Center=�$��0��$��Center=�G� -   $��
KeyTextBox��$��10,0,0,0q�$��1��$��0q�$��Center=� G� PasswordChar$ *��� $��3��$��
39,10,40,5q�.+��� -    ��Name$ SiginButton��$��登录��
���"�"�#��[  