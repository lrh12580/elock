   M S B A M L   `   `   `  ����   DesktopApplication!    DesktopApplication.PageMain   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       N� Title$  PageMain�� � 	Resources    AddButtonStyle �� Property �� Value �� 
TargetType ��Name J� SnapsToDevicePixels  TreeViewStyle �� 
TargetType ��Name	 �� Triggers
 P� Property P� Value �� 
TargetName �� Triggers  tmpForThumb �� 
StartPoint �� EndPoint � Color � Offset �� Padding ]� 
Conditions �� Property �� Value��%�  &	        &	 x     &	     '�� �    7  '�� �    �� .���  � �� �� " ��.R��� $ Chrome��#��z��#��z�� . ._��� #��z��#��z����. # z #��z���� " R�.���  � ��# � ����  � ��
 �������  � ��$ 1q���  � ��$ 1q���  � ��# � ����  � ��$
 Bold)���  � ��$
 Auto=���  � ��$
 Auto=���  �@��PanningMode$
 Both=���  �@��IsFlicksEnabled .  ��  � ��$ Center=���  � �� �� " R�.R��� $ Bd��#��z��#��z�� . ._��� $ _tv_scrollviewer_��#��z����.  |�.  #��z��#��z��# z #��z��.���� 	 P� 
 � {� .  . ��� $ Bd��# � �� � ��P� 
 �@%�IsVirtualizing . . ��� $ _tv_scrollviewer_�� � �� .  P� 
 �@%�IsVirtualizing . . ���  � �� �� .R�$� �� " n�.R��� ._��� $	��rec��$��1������ $ 0.5,0.3&�$	 1,1&�.��� $ White��$
 0.22Q�	 P� 
 �@J�IsMouseOver . . ��� $	 rec�� � ��
 ������� " ��.���  �@�OverridesDefaultStyle . ��  � ��
 �   ���  � ��$ 微软雅黑)���  � ��$ Stretch=���  � �� �� " ��.R�� $��g������ $ 0.5,0&�$ 0.5,1&�.��� $ 	LightGray��$	 0.3Q�� $ 	LightGray��$ 1Q�.+��� $��bd��# z��._��� $��5,4,1,3q�#��z��n� $��PART_HeaderGripper��8��  $��Right=�$��0,0,-1,0q�	 P� 
 �@J�IsMouseOver . . ��� $ g�� � �� �� $ 0.5,0&�$ 0.5,1&�.��� $ White��$	 0.3Q�� $ 	LightGray��$ 1Q��� " ��.���  � ��$ 6q���  � ��$ 2q���  � ��$ 14(� ]�  ��  �@��AlternationIndex$ 1����  �@��
IsSelected .  ��  �@J�IsMouseOver .  ./���  � �� �#e1e1e1��  �@J�Opacity$ 1Q�]�  ��  �@��AlternationIndex$ 0����  �@��
IsSelected .  ��  �@J�IsMouseOver .  ./���  � �� �#b6b8bd��  �@J�Opacity$ 1Q�.���  � ShowGridLines .  ����  �� ImageSource$ Icon/background.jpg�� � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+�� -   $��	SearchBar��$��0��$��0,0,0,10q����	#FFDEDEDE � ColumnDefinitions �� $
��Auto ��� $��2* ��� $��* �.+��� $��0�� �� Orientation$ 
Horizontal=�$
��Left=�$��	20,5,10,5q�.+��� -   $��	AddButton��$��30��$��30��$��0q�#��[  ���� $ Icon/addicon.png�� � ContextMenu �� -   $��contextMenu��.@�m� -   $��选择文件��m� -   $��选择文件夹���� -   $��DecreaseButton��$��30��$��30��$��0q�#��[  $��5,0,0,0q����� $ Icon/decreaseIcon.png���� $��0,5,10,5q�$��2��
�������
�������$��2q� �� CornerRadius$ 12��._��  �� $��* ��� $
��Auto �.+��� -   $��textbox��$
��Left=�$��5,5,0,5q�$��20��$
��Auto��$	��370��$��Center=�$��0q�$��0���� $��1��$��5q�$ 
Horizontal=�$��Right=�.+��� -   $��SearchButton��$��Right=�$��Center=�$��20��$��20��$��0q�$��0,0,5,0q�#��[  ���� $ Icon/search.png��� -	   $��Mainlist��$��1�� �� $��* ��� $
��Auto ��� $��4* �.+�R� -
   $��leftList��$��0��$��1q�$��10,0,0,0q�
���������Z�   �� Color��%2  (��       * SystemColors.HighlightBrushKey+)(��      *$"SystemColors.HighlightTextBrushKey+)(�� (     *1/SystemColors.InactiveSelectionHighlightBrushKey+)(�� ?     *53SystemColors.InactiveSelectionHighlightTextBrushKey+)�� $ Transparent���� $ Green���� $ Transparent���� $ Black�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$
 Gray��    	Direction$  0Q�!   ShadowDepth$! 5Q�"   Opacity$	" 0.5Q�.@�Q� -   $��allfilesItem��$��0,10,0,0q�# Q� 
IsExpanded# . ���� $ 
Horizontal=�.+��� $��40��$��40��$��Icon/allfileIcon.png���� -   $��allfilesText��$��13(�$��全部文件��$��15,0,0,0q�$��Center=�.@�Q� -   $��文档��$��0,10,0,0q����� $ 
Horizontal=�.+��� $��40��$��40��$��Icon/fileIcon.png���� $��文档��$��13(�$��15,0,0,0q�$��Center=�Q� -   $��图片��$��0,10,0,0q����� $ 
Horizontal=�.+��� $��40��$��40��$��Icon/picIcon.png���� $��图片��$��13(�$��15,0,0,0q�$��Center=�Q� -   $��视频��$��0,10,0,0q����� $ 
Horizontal=�.+��� $��40��$��40��$��Icon/videoIcon.png���� $��视频��$��13(�$��15,0,0,0q�$��Center=�Q� -   $��音频��$��0,10,0,0q����� $ 
Horizontal=�.+��� $��40��$��40��$��Icon/musicIcon.png���� $��音频��$��13(�$��15,0,0,0q�$��Center=�Q� -   $��应用��$��0,10,0,0q����� $ 
Horizontal=�.+��� $��40��$��40��$��appIcon.png���� $��应用��$��13(�$��15,0,0,0q�$��Center=�Q� -   $��其它��$��0,10,0,0q����� $ 
Horizontal=�.+��� $��40��$��Icon/elseIcon.png���� $��其它��$��13(�$��15,0,0,0q�$��Center=��� $��2��$��Stretch=�$��Center=�$ �� ShowsPreview$ .  $��1��$��5q����	#FFA0A0A0�� -   $��
mylistview��$��3��$��1q�
�������$��0,0,10,0q�% �� AlternationCount$% 2����Z�    $ Black��$  0Q�$! 3Q�$	" 0.5Q�& �� View& �� -   gridView��.P��� $��	文件名��' �� Width$	' 220��( �� DisplayMemberBinding( �� ) �� Path$) 	File_Name�* �� Mode$* OneWay=��� $��路径��$	' 380��( �� $) 	File_Path�$* OneWay=��� $��Stretch=�$��Bottom=�
�������$��1q�$��2��._��� -   $��	countText��$��Center=�+ �� Padding$+ 	20,5,20,5q�
�������$��Medium"����� 