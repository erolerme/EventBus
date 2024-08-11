using System;
using System.Windows.Forms;
using EventBus.Events;

namespace EventBus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            #region Subscription
            EventBus.Instance.Subscribe<Button1ClickedEvent>(EventBus.EventBusChannelKeys.UI_CHANNEL, Button1Clicked);
            EventBus.Instance.Subscribe<Button2ClickedEvent>(EventBus.EventBusChannelKeys.UI_CHANNEL, Button2Clicked);
            EventBus.Instance.Subscribe<Button3ClickedEvent>(EventBus.EventBusChannelKeys.UI_CHANNEL, Button3Clicked);
            EventBus.Instance.Subscribe<VoidEvent>(EventBus.EventBusChannelKeys.UI_CHANNEL, Button4Clicked);
            #endregion Subscription
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EventBus.Instance.Publish<Button1ClickedEvent>(EventBus.EventBusChannelKeys.UI_CHANNEL, new Button1ClickedEvent { Value = "parameter" });    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EventBus.Instance.Publish<Button2ClickedEvent>(EventBus.EventBusChannelKeys.UI_CHANNEL, new Button2ClickedEvent { Value1 = 2, Value2 = 5 });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EventBus.Instance.Publish<Button3ClickedEvent>(EventBus.EventBusChannelKeys.UI_CHANNEL, new Button3ClickedEvent { ClickedBtn = (Button)sender });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EventBus.Instance.Publish<VoidEvent>(EventBus.EventBusChannelKeys.UI_CHANNEL, new VoidEvent());
        }

        #region SubscriptionFuncs
        private void Button1Clicked(Button1ClickedEvent data)
        {
            textBox1.Text = data.Value;
        }

        private void Button2Clicked(Button2ClickedEvent data)
        {
            textBox2.Text = (data.Value1 * data.Value2).ToString();
        }

        private void Button3Clicked(Button3ClickedEvent data)
        {
            textBox3.Text = data.ClickedBtn.Name;
        }

        private void Button4Clicked(VoidEvent v)
        {
            textBox4.Text = "Void Event";
        }
        #endregion SubscriptionFuncs
    }
}
