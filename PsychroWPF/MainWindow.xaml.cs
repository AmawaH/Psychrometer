using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PsychroWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            dryTermComboBoxItemsList_Init();
            pressureComboBoxList_Init();

        }

        private void dryTermComboBoxItemsList_Init()
        {
            List<int> dryTermBomboBoxList = new();
            for (int i = 0; i < 41; i++)
            {
                dryTermBomboBoxList.Add(i);
            }
            dryTermComboBox.ItemsSource = dryTermBomboBoxList;
            dryTermComboBox.SelectedValue = 20;

        }

        private void pressureComboBoxList_Init()
        {
            List<int> pressureComboBoxList = new();
            for (int i = 720; i < 791; i++)
            {
                pressureComboBoxList.Add(i);
            }
            pressureComboBox.ItemsSource = pressureComboBoxList;
            pressureComboBox.SelectedValue = 760;
        }

        private void dryTermComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<int> wetTermComboBoxList = new();
            int wetComboBoxMax = int.Parse(dryTermComboBox.SelectedValue.ToString());
            for (int i = wetComboBoxMax-20; i < wetComboBoxMax+1; i++)
            {
                wetTermComboBoxList.Add(i);
            }
            wetTermComboBox.ItemsSource = wetTermComboBoxList;
            wetTermComboBox.SelectedValue = wetComboBoxMax;


        }
      
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            speedTextBlock.Text = $"Швидкiсть вiтру(штиль = 0.13, буревiй або психрометр Ассмана = 5): {(e.NewValue):f2} м/c";
            ((Slider)sender).SelectionEnd = e.NewValue;
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            double tempDry, tempWet, pres, speed,  humidity, alfa, pd, pw, dw, dd;
            tempDry = (double.TryParse(dryTermComboBox.SelectedValue.ToString(), out tempDry)) ? tempDry : 20;
            tempWet = (double.TryParse(wetTermComboBox.SelectedValue.ToString(), out tempWet)) ? tempWet : 20;
            pres = (double.TryParse(pressureComboBox.SelectedValue.ToString(), out pres)) ? pres : 760;
            speed = speedSlider.Value;
            alfa = 1e-6 * (593.1 + (135.1 / Math.Sqrt(speed)) + (48 / speed)); //Залежнiсть психрометричного коефiцiєнта вiд швидкостi руху повiтря на м/с, формула Зворикiна
            pd = 4.5845 * Math.Exp((tempDry * (18.678 - (tempDry / 234.5))) / (257.14 + tempDry)); //парцiальний тиск водяної пари при температурi сухого термометра 
            pw = 4.5845 * Math.Exp((tempWet * (18.678 - (tempWet / 234.5))) / (257.14 + tempWet)); //парцiальний тиск водяної пари при температурi вологого термометра
            dd = (288.97 * pd) / (273.15 + tempDry); //густина насиченої водяної пари при температурi сухого термометра, г/м³
            dw = (288.97 * pw) / (273.15 + tempWet); //густина насиченої водяної пари при температурi вологого термометра, г/м³
            humidity = 100 * ((dw / dd) - alfa * pres * (tempDry - tempWet) / dd); //обчислення вiдносної вологостi повiтря
            MessageBox.Show($"Відносна вологість: {humidity:f2}%", "Результат розрахунків", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
