   M S B A M L   `   `   `  ���� B  >Nirvana, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Nirvana.ViewsNirvana.Views  !    Nirvana.Views.LoginSettings   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:Nirvana.Views       � Title$  LoginSettings��$��187.256��$	��300�� � WindowStyle$
 None=� � ShowInTaskbar .   � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � Topmost . ����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FFF9FAFB��	 � Offset$	 0Q�� $ 	#FF9393A0��$	 1Q�.��� 
 � RowDefinitions
 �� $	��11* ��� $	��24* ��� $	��10* ��� $	��24* ��� $��9* ��� $	��25* ��� $��9* ��� $	��27* ��� $	��12* ��� $	��22* ��� $	��14* � � ColumnDefinitions �� $	��18* ��� $	��86* ��� $	��28* ��� $	��64* ��� $	��20* ��� $	��18* ��� $	��47* ��� $	��19* �.+��� -    ��Name$ button��$��Ok��$��3��$��9���� -   $ button1��$��2��$��Cancel��$��5��$��9���� -    ��Name$ Downloader_txt��$��5��$��2��$��1�� �� TextWrapping$ NoWrap=�#��� ������ *
Downloader+ �� SelectionBrush Z� �� -   $ 
UserID_txt��$��5��$��2��$��3��$ NoWrap=�#��� ������ *
UserId_1+ Z� �� -   $ UserID2_txt��$��5��$��2��$��5��$ NoWrap=�#��� �� �� 	IsEnabled . ���� *
UserId_2+ Z� �� -    ��Name$ Downloader_lbl��$��
Downloader��$��1��$��1���� -   $ 
UserID_lbl��$��userId1��$��3��$��1���� -   $ UserID2_lbl��$��userId2��$��1��$��5���� -	   $ FilePath_lbl��$��Путь��$��1��$��7��$��0,1,0,0q��� -
   $ FilePath_txt��$��4��$��2��$��7��$ NoWrap=�#��� �� . $��0,1,10,0q����� *
Filepath+ Z� �� -   $ Search��$	��...��$��6��$��0,1,0,0q�$��7��