   M S B A M L   `   `   `  ���� B  >Nirvana, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Nirvana.PropertiesNirvana.Properties  -clr-namespace:Nirvana.ViewsNirvana.Views  ?$clr-namespace:Nirvana.Models.TaskBarNirvana.Models.TaskBar  c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity     Nirvana.Views.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .p clr-namespace:Nirvana.Properties     -localclr-namespace:Nirvana.Views     9commands$clr-namespace:Nirvana.Models.TaskBar     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     \ XHardcodet.Wpf.TaskbarNotification, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null)tb http://www.hardcodet.net/taskbar  -     �Name$  
headWindow��$	��640�� � Title$ НирванаБот�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � AllowsTransparency .  � ShowInTaskbar .  
 � Top �� !  Nirvana.Properties.Settings   Default �� Source# Z 	 �� Path$		 Top�
 �� Mode$
 TwoWay=� � Left �� # Z $
	 Left�$
 TwoWay=����� # Z $	 Height�$
 TwoWay=� � 	Resources    listBoxStyle1 �� 
TargetType �� 	Resources �� Color��%b   &	        �� " �� ���� $ Transparent��(��       * SystemColors.HighlightBrushKey+).  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ MouseLeftButtonDown��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *DragMoveCommand+   CommandParameter ��  �� ElementName$ 
headWindow��  $ Loaded��.    �� *LoadedCommand+ �� $ 
headWindow������ $ White������ $ 	#F2CAD4DE��.��� .+�3  -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon  -     Name$	 tbi��$	��Top=�   
IconSource$- '/Nirvana;component/Resources/258211.ico��   LeftClickCommand �� *TaskBarCommand+    LeftClickCommandParameter �� $ 
headWindow�� � ContextMenu �� 
�������.@�m� $��Развернуть�� m� Command 4   .Nirvana.Models.TaskBar.ShowSampleWindowCommand   m� CommandParameter �� m� $��Свернуть�� 4   .Nirvana.Models.TaskBar.HideSampleWindowCommand   �� �� m� $��
Выход�� /	   )Nirvana.Models.TaskBar.CloseWindowCommand	   ��  m� Icon �� $��16��$��16��$0��*pack://application:,,,/Resources/close.jpg���� -    ��Name$ groupBox��$��Состав пати��$
��Left=�$	��Top=�$	��363��$	��371��$��12,0,0,0q�.���� $
��Left=� �� Orientation$ Vertical=�$	��371��$��	4,0,-16,0q�.+��� $��
0, 7, 0, 0q�$
��Left=�$ 
Horizontal=�.+��� -     ��Name$  lbl_1��$
��ПЛ��$��10, 2, 0, 0q�$��Center=�$
��Left=�$��20��$��0q�$��30���� -   ! ��Name$! cmbx_pl��$
��Left=�$��79, 0, 0 ,0q�$��Center=�$	��120��$��22��" K� ZIndex$" 1��$��0q�$��Center=�$��Center=����� .R��� ���� *Name+   $ SelectionChanged��.    �� *%#HeadComboboxSelectionChangedCommand+ �� $ cmbx_pl���� -   $��change_pl_chk_box��# � ToolTip$:# 4Смена пла при пересборе пати��$��10, 2, 0, 0q�$
��Left=�$��Center=�$ d� 	IsChecked$ �� *CheckBoxSettings.ChangePl+.��ПЛ�� -   % ��Name$% start_stop_btn��$��
Старт��$
��Left=�$��10, 0, 0 ,0q�$��Center=�$��60������ *StartAndStopButtonCommand+���� $ start_stop_btn���� $��
0, 7, 0, 0q�$
��Left=�$ 
Horizontal=�.+��� -   $  lbl_2��$��Открывашка 1��$
��Left=�$��10, 2, 0, 0q�$��Center=�$��20��$��0q��� -	   $! cmbx_otkr_1��$��30, 0, 0 ,0q�$
��Left=�$��Center=�$	��120��$��22��$" 2��$��Center=�$��Center=�$��0q����� .R��� ���� *Name+   $ SelectionChanged��.    �� *%#HeadComboboxSelectionChangedCommand+ �� $ cmbx_otkr_1���� -
   & ��Name$& ckbx_1_1��$��С��$��30, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_1_1+���� *ChangeFlagSCommand+���� $ cmbx_otkr_1��$	 SelectedItem��� -   $& ckbx_1_2��$��Б��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_1_2+���� *ChangeFlagBCommand+���� $ cmbx_otkr_1��$	 SelectedItem��� -   $& ckbx_1_3��$��Д��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_1_3+���� *ChangeFlagDCommand+���� $ cmbx_otkr_1��$	 SelectedItem��� $��
0, 7, 0, 0q�$
��Left=�$ 
Horizontal=�.+��� -   $  lbl_3��$��Открывашка 2��$
��Left=�$��10, 2, 0, 0q�$��Center=�$��20��$��0q��� -   $! cmbx_otkr_2��$��30, 0, 0 ,0q�$
��Left=�$��Center=�$	��120��$��22��$" 3��$��0q�$��Center=�$��Center=����� .R��� ���� *Name+   $ SelectionChanged��.    �� *%#HeadComboboxSelectionChangedCommand+ �� $ cmbx_otkr_2���� -   $& ckbx_2_1��$��С��$
��Left=�$��30, 0, 0 ,0q�$��Center=�$ �� *CheckBoxSettings.Check_2_1+���� *ChangeFlagSCommand+���� $ cmbx_otkr_2��$	 SelectedItem��� -   $& ckbx_2_2��$��Б��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_2_2+���� *ChangeFlagBCommand+���� $ cmbx_otkr_2��$	 SelectedItem��� -   $& ckbx_2_3��$��Д��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_2_3+���� *ChangeFlagDCommand+���� $ cmbx_otkr_2��$	 SelectedItem��� $��
0, 7, 0, 0q�$
��Left=�$ 
Horizontal=�.+��� -   $  lbl_4��$��Открывашка 3��$
��Left=�$��10, 2, 0, 0q�$��Center=�$��20��$��0q��� -   $! cmbx_otkr_3��$
��Left=�$��30, 0, 0 ,0q�$��Center=�$	��120��$��22��$" 4��$��Center=�$��Center=�$��0q����� .R��� ���� *Name+   $ SelectionChanged��.    �� *%#HeadComboboxSelectionChangedCommand+ �� $ cmbx_otkr_3���� -   $& ckbx_3_1��$��С��$
��Left=�$��30, 0, 0 ,0q�$��Center=�$ �� *CheckBoxSettings.Check_3_1+���� *ChangeFlagSCommand+���� $ cmbx_otkr_3��$	 SelectedItem��� -   $& ckbx_3_2��$��Б��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_3_2+���� *ChangeFlagBCommand+���� $ cmbx_otkr_3��$	 SelectedItem��� -   $& ckbx_3_3��$��Д��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_3_3+���� *ChangeFlaDSCommand+���� $ cmbx_otkr_3��$	 SelectedItem��� $��
0, 7, 0, 0q�$
��Left=�$ 
Horizontal=�.+��� -   $  lbl_5��$��Открывашка 4��$
��Left=�$��10, 2, 0, 0q�$��Center=�$��20��$��0q��� -   $! cmbx_otkr_4��$
��Left=�$��Center=�$��30, 0, 0 ,0q�$	��120��$��22��$" 5��$��Center=�$��Center=�$��0q����� .R��� ���� *Name+   $ SelectionChanged��.    �� *%#HeadComboboxSelectionChangedCommand+ �� $ cmbx_otkr_4���� -   $& ckbx_4_1��$��С��$
��Left=�$��30, 0, 0 ,0q�$��Center=�$ �� *CheckBoxSettings.Check_4_1+���� *ChangeFlagSCommand+���� $ cmbx_otkr_4��$	 SelectedItem��� -   $& ckbx_4_2��$��Б��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_4_2+���� *ChangeFlagBCommand+���� $ cmbx_otkr_4��$	 SelectedItem��� -   $& ckbx_4_3��$��Д��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_4_3+���� *ChangeFlagDCommand+���� $ cmbx_otkr_4��$	 SelectedItem��� $��
0, 7, 0, 0q�$
��Left=�$ 
Horizontal=�.+��� -   $  lbl_6��$��Открывашка 5��$
��Left=�$��10, 2, 0, 0q�$��Center=�$��20��$��0q��� -   $! cmbx_otkr_5��$
��Left=�$��30, 0, 0 ,0q�$��Center=�$	��120��$��22��$" 6��$��0q�$��Center=�$��Center=����� .R��� ���� *Name+   $ SelectionChanged��.    �� *%#HeadComboboxSelectionChangedCommand+ �� $ cmbx_otkr_5���� -   $& ckbx_5_1��$��С��$
��Left=�$��30, 0, 0 ,0q�$��Center=�$ �� *CheckBoxSettings.Check_5_1+���� *ChangeFlagSCommand+���� $ cmbx_otkr_5��$	 SelectedItem��� -   $& ckbx_5_2��$��Б��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_5_2+���� *ChangeFlagBCommand+���� $ cmbx_otkr_5��$	 SelectedItem��� -    $& ckbx_5_3��$��Д��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_5_3+���� *ChangeFlagDCommand+���� $ cmbx_otkr_5��$	 SelectedItem��� $��
0, 7, 0, 0q�$
��Left=�$ 
Horizontal=�.+��� -!   $  lbl_7��$��Открывашка 6��$
��Left=�$��10, 2, 0, 0q�$��Center=�$��20��$��0q��� -"   $! cmbx_otkr_6��$
��Left=�$��30, 0, 0 ,0q�$��Center=�$	��120��$��22��$" 6��$��Center=�$��Center=����� .R��� ���� *Name+   $ SelectionChanged��.    �� *%#HeadComboboxSelectionChangedCommand+ �� $ cmbx_otkr_6���� -#   $& ckbx_6_1��$��С��$
��Left=�$��30, 0, 0 ,0q�$��Center=�$ �� *CheckBoxSettings.Check_6_1+���� *ChangeFlagSCommand+���� $ cmbx_otkr_6��$	 SelectedItem��� -$   $& ckbx_6_2��$��Б��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_6_2+���� *ChangeFlagBCommand+���� $ cmbx_otkr_6��$	 SelectedItem��� -%   $& ckbx_6_3��$��Д��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_6_3+���� *ChangeFlagDCommand+���� $ cmbx_otkr_6��$	 SelectedItem��� $��
0, 7, 0, 0q�$
��Left=�$ 
Horizontal=�.+��� -&   $  lbl_8��$��Открывашка 7��$
��Left=�$��10, 2, 0, 0q�$��Center=�$��20��$��0q��� -'   $! cmbx_otkr_7��$
��Left=�$��30, 0, 0 ,0q�$��Center=�$	��120��$��22��$" 6��$��Center=�$��Center=����� .R��� ���� *Name+   $ SelectionChanged��.    �� *%#HeadComboboxSelectionChangedCommand+ �� $ cmbx_otkr_7���� -(   $& ckbx_7_1��$��С��$
��Left=�$��30, 0, 0 ,0q�$��Center=�$ �� *CheckBoxSettings.Check_7_1+���� *ChangeFlagSCommand+���� $ cmbx_otkr_7��$	 SelectedItem��� -)   $& ckbx_7_2��$��Б��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_7_2+���� *ChangeFlagBCommand+���� $ cmbx_otkr_7��$	 SelectedItem��� -*   $& ckbx_7_3��$��Д��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_7_3+���� *ChangeFlagDCommand+���� $ cmbx_otkr_7��$	 SelectedItem��� $��
0, 7, 0, 0q�$
��Left=�$ 
Horizontal=�.+��� -+   $  lbl_9��$��Открывашка 8��$
��Left=�$��10, 2, 0, 0q�$��Center=�$��20��$��0q��� -,   $! cmbx_otkr_8��$
��Left=�$��30, 0, 0 ,0q�$��Center=�$	��120��$��22��$" 6��$��Center=�$��Center=����� .R��� ���� *Name+   $ SelectionChanged��.    �� *%#HeadComboboxSelectionChangedCommand+ �� $ cmbx_otkr_8���� --   $& ckbx_8_1��$��С��$
��Left=�$��30, 0, 0 ,0q�$��Center=�$ �� *CheckBoxSettings.Check_8_1+���� *ChangeFlagSCommand+���� $ cmbx_otkr_8��$	 SelectedItem��� -.   $& ckbx_8_2��$��Б��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_8_2+���� *ChangeFlagBCommand+���� $ cmbx_otkr_8��$	 SelectedItem��� -/   $& ckbx_8_3��$��Д��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_8_3+���� *ChangeFlagDCommand+���� $ cmbx_otkr_8��$	 SelectedItem��� $��
0, 7, 0, 0q�$
��Left=�$ 
Horizontal=�.+��� -0   $  lbl_10��$��Открывашка 9��$
��Left=�$��10, 2, 0, 0q�$��Center=�$��20��$��0q��� -1   $! cmbx_otkr_9��$
��Left=�$��30, 0, 0 ,0q�$��Center=�$	��120��$��22��$" 6��$��Center=�$��Center=����� .R��� ���� *Name+   $ SelectionChanged��.    �� *%#HeadComboboxSelectionChangedCommand+ �� $ cmbx_otkr_9���� -2   $& ckbx_9_1��$��С��$
��Left=�$��30, 0, 0 ,0q�$��Center=�$ �� *CheckBoxSettings.Check_9_1+���� *ChangeFlagSCommand+���� $ cmbx_otkr_9��$	 SelectedItem��� -3   $& ckbx_9_2��$��Б��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_9_2+���� *ChangeFlagBCommand+���� $ cmbx_otkr_9��$	 SelectedItem��� -4   $& ckbx_9_3��$��Д��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_9_3+���� *ChangeFlagDCommand+���� $ cmbx_otkr_9��$	 SelectedItem��� $��
0, 7, 0, 0q�$
��Left=�$ 
Horizontal=�.+��� -5   $  lbl_11��$��Пересборщик��$
��Left=�$��10, 2, 0, 0q�$��Center=�$��20��$��0q��� -6   $! 	cmbx_sham��$
��Left=�$��31, 0, 0 ,0q�$��Center=�$	��120��$��22��$" 7��$��Center=�$��Center=����� .R��� ���� *Name+   $ SelectionChanged��.    �� *%#HeadComboboxSelectionChangedCommand+ �� $ 	cmbx_sham���� -7   $& 	ckbx_10_1��$��С��$
��Left=�$��30, 0, 0 ,0q�$��Center=�$ �� *CheckBoxSettings.Check_10_1+���� *ChangeFlagSCommand+���� $ 	cmbx_sham��$	 SelectedItem��� -8   $& 	ckbx_10_2��$��Б��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_10_2+���� *ChangeFlagBCommand+���� $ 	cmbx_sham��$	 SelectedItem��� -9   $& 	ckbx_10_3��$��Д��$��
5, 0, 0, 0q�$
��Left=�$��Center=�$ �� *CheckBoxSettings.Check_10_3+���� *ChangeFlagDCommand+���� $ 	cmbx_sham��$	 SelectedItem��� -:   $ 	groupBox1��$��Команды��$
��Left=�$��
396,84,0,0q�$	��Top=�$	��248��$	��225��.���� .+��� $��0, 10, 0, 0q�$��20��$ 
Horizontal=�.+��� -;   $  	lbl_cmd_1��$��20��$	��140��$$��Пересбор в нирку��$
��Left=�$	��Top=�' J� RenderTransformOrigin$' 0.368,-0.654&�$��0q�$��14(��� -<   ( ��Name$( peresbor_v_nirku��$��20��$��70��$��0q����� *#!CheckBoxSettings.Peresbor_v_nirku+�� $��20��$��0, 10, 0, 0q�$ 
Horizontal=�.+��� -=   $  	lbl_cmd_2��$��20��$	��140��$��Физнирка��$
��Left=�$	��Top=�$' 0.368,-0.654&�$��0q�$��14(��� ->   $( fiznirka��$��20��$��70��$��0q����� *CheckBoxSettings.Fiznirka+�� $��20��$��0, 10, 0, 0q�$ 
Horizontal=�.+��� -?   $  	lbl_cmd_3��$��20��$	��140��$��Пересбор пати��$
��Left=�$	��Top=�$' 0.368,-0.654&�$��0q�$��14(��� -@   $( peresbor��$��20��$��70��$��0q����� *CheckBoxSettings.Peresbor+�� $��20��$��0, 10, 0, 0q�$ 
Horizontal=�.+��� -A   $  	lbl_cmd_4��$��20��$	��140��$��Пати, пл и тп��$
��Left=�$	��Top=�$' 0.368,-0.654&�$��0q�$��14(��� -B   $( party_and_pl��$��20��$��70��$��0q����� *CheckBoxSettings.Party_and_pl+�� $��20��$��0, 10, 0, 0q�$ 
Horizontal=�.+��� -C   $  	lbl_cmd_5��$��20��$	��140��$��
Ребаф��$
��Left=�$	��Top=�$' 0.368,-0.654&�$��0q�$��14(��� -D   $( rebaf��$��20��$��70��$��0q����� *CheckBoxSettings.Rebaf+�� $��20��$��0, 10, 0, 0q�$ 
Horizontal=�.+��� -E   $  	lbl_cmd_6��$��20��$	��140��$(��"Следовать за целью��$
��Left=�$	��Top=�$' 0.368,-0.654&�$��0q�$��14(��� -F   $( to_him��$��20��$��70��$��0q����� *CheckBoxSettings.To_him+�� $��20��$��0, 10, 0, 0q�$ 
Horizontal=�.+��� -G   $  	lbl_cmd_7��$��20��$	��140��$��Остановиться��$
��Left=�$	��Top=�$' 0.368,-0.654&�$��0q�$��14(��� -H   $
( stop��$��20��$��70��$��0q����� *CheckBoxSettings.Stop+�� -I   $% button��$,��&Обновить список окон��$
��Left=�$��396,338,0,0q�$	��Top=�$	��225��$��25��$��0q����� * RefreshClientCollectionCommand+���� $ 
headWindow���� -J   $% save_btn��$��Сохранить��$
��Left=�$��
396,10,0,0q�$	��Top=�$	��110��$' 0.16,-0.227&�$��31��#��� ������ *SaveSettingsCommand+�� -K   $% hide_btn��$��Свернуть��$
��Left=�$��
396,46,0,0q�$	��Top=�$	��110��$' 0.16,-0.227&�$��31��#��� ������ *TaskBarCommand+���� $ 
headWindow���� -L   $% 
loging_btn��$��Запуск��$
��Left=�$��
511,10,0,0q�$	��Top=�$	��110��$' 0.16,-0.227&�$��31��#��� ������ *OpenLoginWindowCommand+���� $ 
headWindow���� -M   $% 
offset_btn��$��Офсеты��$
��Left=�$��
511,46,0,0q�$	��Top=�$	��110��$' 0.16,-0.227&�$��31��#��� ������ *OpenOffsetWindowCommand+� -N   ) �Name$) grid_loging��$
��Left=�$	��353��$��
626,10,0,0q�$	��Top=�$	��247��$y�Hidden=�.+��� -O   $ groupBox_logging��$%��Список аккаунтов��$
��Left=�$��	10,10,0,0q�$	��Top=�$	��333��$	��227��.��� .+�� $��0,0,0,66q�.+��� -P   * ��Name$* listAccount�����	#F2CAD4DE$��Hidden=�$��Disabled=�#���   ���� *
Accounts+���� .R��� $ 
Horizontal=����	#F2CAD4DE$��25��.+��� $	��120��+ �� TextAlignment$+ Center=�$��20�����	#F2CAD4DD���� *
Nickname+�� $��80��$+ Center=�$��10, 0, 0, 0q�$��20�����	#F2CAD4DD���� *Server+� $��
0,259,0,10q�.+��� $ 
Horizontal=�$��Center=�.+��� $��40��$��40��$��0q�|�.  $��
5, 0, 5, 0q����� *DeleteAccountCommand+���� $ listAccount��$	 SelectedItem����� .R��� $ 
Horizontal=�.+��� $1��+pack://application:,,,/Resources/delete.jpg��$
��Fill=�.��/>�� $��40��$��40��$��
5, 0, 5, 0q�$��0q�|�.  ���� *AddAccountCommand+���� $ listAccount��$	 SelectedItem����� .R��� $ 
Horizontal=�.+��� $.��(pack://application:,,,/Resources/add.jpg��$
��Fill=�.��/>�� $��40��$��40��$��
5, 0, 5, 0q�$��0q�|�.  ���� *LoginSettingsOpneCommand+���� $ listAccount��$	 SelectedItem����� .R��� $ 
Horizontal=�.+��� $.��(pack://application:,,,/Resources/set.jpg��$
��Fill=�.��/>�� $��40��$��40��$��
5, 0, 5, 0q�$��0q�|�.  ���� *PlayCommand+���� $ listAccount��$	 SelectedItem����� .R��� $ 
Horizontal=�.+��� $/��)pack://application:,,,/Resources/play.jpg��$
��Fill=�.��/>