using Xamarin.Forms;

namespace CocosSharpFrameAnimation
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var cocosAnime = new CocosAnime();

            // CocosSharpView
            this.CocosView.ViewCreated = cocosAnime.Create;

            // AAボタン
            var aaTap = new TapGestureRecognizer();
            aaTap.Tapped += (sender, e) => cocosAnime.ChangeAAAnimation();
            this.ImageButtonAA.GestureRecognizers.Add(aaTap);
            this.ImageButtonAA.Source = cocosAnime.ImageAA;

            // ねこボタン
            var catTap = new TapGestureRecognizer();
            catTap.Tapped += (sender, e) => cocosAnime.ChangeCatAnimation();
            this.ImageButtonCat.GestureRecognizers.Add(catTap);
            this.ImageButtonCat.Source = cocosAnime.ImageCat;

            // 倍速逆再生ボタン
            var fastRewindTap = new TapGestureRecognizer();
            fastRewindTap.Tapped += (sender, e) => cocosAnime.FastRewindAnimation();
            this.ImageButtonFastRewind.GestureRecognizers.Add(fastRewindTap);
            this.ImageButtonFastRewind.Source = cocosAnime.ImageFastRewind;

            // 逆再生ボタン
            var rewindTap = new TapGestureRecognizer();
            rewindTap.Tapped += (sender, e) => cocosAnime.RewindAnimation();
            this.ImageButtonRewind.GestureRecognizers.Add(rewindTap);
            this.ImageButtonRewind.Source = cocosAnime.ImageRewind;

            // 停止ボタン
            var stopTap = new TapGestureRecognizer();
            stopTap.Tapped += (sender, e) => cocosAnime.StopAnimation();
            this.ImageButtonStop.GestureRecognizers.Add(stopTap);
            this.ImageButtonStop.Source = cocosAnime.ImageStop;

            // 再生ボタン
            var playTap = new TapGestureRecognizer();
            playTap.Tapped += (sender, e) => cocosAnime.PlayAnimation();
            this.ImageButtonPlay.GestureRecognizers.Add(playTap);
            this.ImageButtonPlay.Source = cocosAnime.ImagePlay;

            // 倍速再生ボタン
            var fastFowardTap = new TapGestureRecognizer();
            fastFowardTap.Tapped += (sender, e) => cocosAnime.FastFowardAnimation();
            this.ImageButtonFastFoward.GestureRecognizers.Add(fastFowardTap);
            this.ImageButtonFastFoward.Source = cocosAnime.ImageFastFoward;
        }
    }
}
