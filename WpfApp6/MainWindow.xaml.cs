using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Drink> drinks = new List<Drink>();
        List<Orderltem> order = new List<Orderltem>();
        string takeout;
        public MainWindow()
        {
            InitializeComponent();
            addNewDrink(drinks);
            DisplayDrink(drinks);
        }
        private void DisplayDrink(List<Drink> myDrink)
        {
            foreach(Drink d in drinks)
            {
                StackPanel sp = new StackPanel();
                CheckBox cb = new CheckBox();
                Slider sl = new Slider();
                Label lb = new Label();

                sp.Orientation = Orientation.Horizontal;

                cb.Content = d.Name + d.Size + d.Price;
                cb.Width = 150;
                cb.Margin = new Thickness(5);

                sl.Value = 0;
                sl.Width = 100;
                sl.Minimum = 0;
                sl.Maximum = 20;
                sl.Margin = new Thickness(5);
                sl.IsSnapToTickEnabled = true;
                sl.AutoToolTipPlacement = AutoToolTipPlacement.BottomRight;

                lb.Width = 50;
                lb.Content = 0;

                sp.Children.Add(cb);
                sp.Children.Add(sl);
                sp.Children.Add(lb);

                Binding myBinding = new Binding("Value");
                myBinding.Source = sl;
                lb.SetBinding(ContentProperty, myBinding);
                
                stackpanel_DrinkMenu.Children.Add(sp);
            }
        }

        private void addNewDrink(List<Drink> myDrink)
        {
            myDrink.Add(new Drink() { Name = "咖啡", Size = "大杯", Price = 60 });
            myDrink.Add(new Drink() { Name = "咖啡", Size = "小杯", Price = 50 });
            myDrink.Add(new Drink() { Name = "紅茶", Size = "大杯", Price = 30 });
            myDrink.Add(new Drink() { Name = "紅茶", Size = "小杯", Price = 20 });
            myDrink.Add(new Drink() { Name = "綠茶", Size = "大杯", Price = 25 });
            myDrink.Add(new Drink() { Name = "綠茶", Size = "小杯", Price = 20 });
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            PlaceOrder(order);

            DisplayOrderDetail(order);
        }

        private void DisplayOrderDetail(List<Orderltem> myOrder)
        {
            displayTextBlock.Text = $"您所訂購的商品項為:{takeout}\n";

            int i = 1;
            int total = 0;
            foreach (Orderltem item in order)
            {
                total += item.SubTotal;
                Drink drinkItem = drinks[item.Index];
                displayTextBlock.Text += $"第{i}項:{drinkItem.Name}{drinkItem.Size}、單價{drinkItem.Price}元、總共{item.Quantity}杯、小計{item.SubTotal}元。\n";
                i++;
            }

            if (total >= 500)
            {
                displayTextBlock.Text += $"此次訂單總共{total}元。\n";
                double a;
                a = ((total) * 0.8);
                displayTextBlock.Text += $"訂購滿500打8折{a}元。";
            }
            else
            {
                if (total >= 300)
                {
                    displayTextBlock.Text += $"此次訂單總共{total}元。\n";
                    double a;
                    a = ((total) * 0.85);
                    displayTextBlock.Text += $"訂購滿300打85折{a}元。";
                }
                else {
                    if (total >= 200)
                    {
                        displayTextBlock.Text += $"此次訂單總共{total}元。\n";
                        double a;
                        a = ((total) * 0.9);
                        displayTextBlock.Text += $"訂購滿200打9折{a}元。";
                    }
                    else
                    {
                        displayTextBlock.Text += $"此次訂單總共{total}元。";
                    }
                }
                

            }
            
            
        }
        private void PlaceOrder(List<Orderltem> myOrder)
        {
            myOrder.Clear();

            for(int i=0; i<stackpanel_DrinkMenu.Children.Count; i++)
            {
                StackPanel sp = stackpanel_DrinkMenu.Children[i] as StackPanel;
                CheckBox cb = sp.Children[0] as CheckBox;
                Slider sl = sp.Children[1] as Slider;
                int quantity = Convert.ToInt32(sl.Value);

                if(cb.IsChecked == true && quantity != 0)
                {
                    int price = drinks[i].Price;
                    int subtotal = price * quantity;
                    myOrder.Add(new Orderltem() { Index = i, Quantity = quantity, SubTotal = subtotal });
                }
            }
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.IsChecked == true) takeout = rb.Content.ToString();
        }
    }
}
