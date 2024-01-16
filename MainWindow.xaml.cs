using System;
using System.Collections.Generic;
using System.IO;
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

namespace wpfoyun
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int karew=15;//hedefin genişliği
        int kareh=15;//hedefin yüksekliği
        int formw=500;//canvasın genişliği
        int formy=500;//canvasın yüksekliği
        int dikdw=50;//dikdörtgenin genişliği
        int dikdh=10;//dikdörtgenin yüksekliği
        int i=0,speed=50,puan=0,maxp;

        Rectangle dikd = new Rectangle();//dikdörtgeni oluşturuyoruz
            
        Rectangle kare= new Rectangle();//hedefi oluşturuyoruz
        
        
        public MainWindow()
        {
            InitializeComponent();
            can.Children.Add(dikd);//dikdörtgeni canvasa ekliyoruz
            can.Children.Add(kare);//hedefi canvasa ekliyoruz
            
            karee();
            dikdd();
            Loaded+=tas;
            maxp=Int16.Parse(File.ReadAllText("max.txt"));

        }

        private async void tas(object sender,RoutedEventArgs e)
        {//hedefimizin sürekli aşağı düşmesini sağladığımız yer async:başladığında diğer işlemlerin bu işlemin bitmesini beklemesini sağlar
            while(true)
            {
                await Task.Delay(speed);//speed kadar async program beklenir
                dus();
            }
        }
        
        private void dus()
        {
            
          
            if((Canvas.GetTop(kare)>320 && Canvas.GetTop(kare)<340)&&(Canvas.GetLeft(kare)>=Canvas.GetLeft(dikd)-10 && (Canvas.GetLeft(kare)<=Canvas.GetLeft(dikd)+50)) ){
                karee(); //hedefi tuttuğumuzda gerçekleşecek işlemler
                puan=puan+1;
                if(puan>maxp)File.WriteAllText("max.txt",puan.ToString());
                speed=speed-1;
                skor.Content="Puan : "+puan.ToString()+"\n" +"Max Puan : "+ File.ReadAllText("max.txt");
                }
            
            else if(Canvas.GetTop(kare)<formy){ 
            Canvas.SetTop(kare,Canvas.GetTop(kare)+10);
            
            }else if(Canvas.GetTop(kare)>300){//hedefi tutamadığımızda gerçekleşecek işlemler
                
                if(i==0)
                {MessageBox.Show("Kaybettin!");
                i++;}

            }
        }

        public void hareket()//hedefin düşmesini sağlıyor
        {
             
            if(Canvas.GetLeft(kare)!=0) 
            {
                Canvas.SetLeft(kare,Canvas.GetLeft(kare)-10);
            }
            Canvas.SetLeft(kare,480);
        }

        
        private void karee()//hedefin özelliklerini ve konumunu belirlediğimiz yer
        {
            
            Random rnd=new Random();
            int y=rnd.Next(0,485);
            int x=0;
            kare.Height=kareh;
            kare.Width=karew;
            kare.Stroke=Brushes.Black;
            kare.Fill=Brushes.White;
            Canvas.SetLeft(kare,y);
            Canvas.SetTop(kare,x);

        }
        private void dikdd()//dikdörtgeni özelliklerini ve başlangıç konumunu belirlediğimiz yer
        {
            dikd.Width=dikdw;
            dikd.Height=dikdh;
            dikd.Stroke=Brushes.Black;
            dikd.Fill=Brushes.Blue;
            Canvas.SetLeft(dikd,formw/2);
            Canvas.SetTop(dikd,350);
        }
      
        public async void Window_KeyDown(object sender, KeyEventArgs e)
    {//tuşlara bastığımızda vermelerini istediğimiz tepkiler
        
        switch(e.Key){
        case Key.Left:
            
            if((Canvas.GetLeft(dikd)-10)>0) Canvas.SetLeft(dikd,Canvas.GetLeft(dikd)-10);
            else Canvas.SetLeft(dikd,450);
            break;
         case Key.Right:
            if((Canvas.GetLeft(dikd)+10)<460) Canvas.SetLeft(dikd,Canvas.GetLeft(dikd)+10); 
            else Canvas.SetLeft(dikd,0);
            break;
        
            
        }
    }
  
    }
}

