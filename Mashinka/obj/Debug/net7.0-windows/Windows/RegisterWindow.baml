   M S B A M L   `   `   `  ����C  ?Mashinka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:WpfApp17WpfApp17  5   	   %    WpfApp17.Windows.RegisterWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:WpfApp17     5   	     � Title$  Регистарция��5   	   $	��450��6   $	��800��6*    � 	Resources 5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $/ )pack://application:,,,/Static/Styles.xaml?�6%   %    5      .��5      (� .��5   
   � $	��450��6   $	��800��6   .+�5      �� -   5         ��Name$ textboxLogin��5      $��
205,51,0,0q�6,    �� TextWrapping$
 Wrap=�6@   $
��Left=�6T   �� 5      -   5        $ textboxEmail��5      $��207,314,0,0q�6,   $
 Wrap=�6A   $
��Left=�6U   �� 5       J� IsHitTestVisible$ False��6   $��
Почта��61   $	��Top=�6>   $
��Left=�6V   $��213,314,0,0q�6q   ���#b3b3b3$��26(�6�   $��42��6�   $	��252��6�   ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � y�	 �� Value$	 	Collapsed=�67   
 �� Triggers
 5      �� 5       �� Binding 6*   �� *Text+ �� ElementName$ textboxEmail�� �� Value  6]   .a�5   "   ��  � y�$	 Visible=�6?   5      5      5       �� 5!      -   5        $ textboxCode��5!      $��517,237,0,0q�6+   $
 Wrap=�6@   $
��Left=�6T   �� 5"      $ False��6   $��Код��61   $	��Top=�6<   $
��Left=�6T   $��523,237,0,0q�6o   ���#b3b3b3$��26(�6�   $��43��6�   $	��252��6�   ��5#      �� 5$      " ��6   .�5%      ��  � y�$	 	Collapsed=�67   
 5&      �� 5'       6*   �� *Text+$ textboxCode��  6\   .a�5(   "   ��  � y�$	 Visible=�6?   5)      5+      5-      G� 5.      -   5         G�Name$ PasswordBoxOne��5.      $��205,134,0,0q�62   $
��Left=�6G   G� 5/      -   5        $ PasswordBoxTwo��5/      $��205,226,0,0q�62   $
��Left=�6G   �� 50      -   5         ��Name$ 	RegButton��50      $��Регистрация��6(   $��207,376,0,0q�6>   $
��Left=�6k   �� 51      -   5        $ 
BackButton��51      $��
Назад��6)   $��0,6,0,0q�69   $
��Left=�6c   $��90��6~   �� 52      $��
Логин��6   $
��Left=�6$   $��
295,14,0,0q�6?   �� 53      $��Пароль��6   $
��Left=�6%   $��
288,99,0,0q�6@   �� 54      $%��Повторите пароль��6   $
��Left=�6/   $��227,186,0,0q�6J   $	��218��6_   �� 55      -   5        $ buttonVerifyCode��55      $��Подтвердить��6/   $��517,331,0,0q�6E   $
��Left=�6y   �� 56      $��
Почта��6   $
��Left=�6$   $��281,276,0,0q�6?   $	��100��6T   58      59      5:      