   M S B A M L   `   `   `  ����B  >Nirvana, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Nirvana.ViewsNirvana.Views  5   	       Nirvana.Views.GetSmsCode   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Nirvana.Views     5   	     � Title$  
GetSmsCode��5   	   $��61.333��6   $	��150��6,    � 
ResizeMode$ NoResize=�68    � WindowStyle$
 None=�6N    � WindowStartupLocation$ CenterScreen=�6a   ��5	      �� 5
   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ 	#FFBBDDEA��6    � Offset$ 0Q�6-   � 5      $ 	#FFDDE4E6��6   $ 1Q�6-   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��15* �6   �� 5      $
��122* �6   �� 5      $	��15* �6   	 � RowDefinitions	 5   
   �� 5      $	��30* �6   �� 5      $	��20* �6   �� 5      $��8* �6   .+�5   
   �� -   5        
 ��Name$
 smsLabel��5      $'��!Введите код из СМС��6"    J� RenderTransformOrigin$ 0.482,0.231&�6?   $��Center=�6c   $��Center=�6~   $��14,2,1,2q�6�   $��2��6�   $��26��6�   $	��120��6�   �� 5   
   -   5         ��Name$ smsBox��5      $��21��6"   $��0,0,1,0q�6.   $��1��6?    �� TextWrapping$
 Wrap=�6L   $	��Top=�6`   $��1��6x   $��2��6�   ���	#FFE6E6F0 �� SelectionBrush 6�   Z� 6�   5      5      