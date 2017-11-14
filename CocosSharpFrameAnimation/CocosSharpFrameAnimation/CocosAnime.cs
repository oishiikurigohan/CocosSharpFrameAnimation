using CocosSharp;
using System;
using System.Collections.Generic;

namespace CocosSharpFrameAnimation
{
    public class CocosAnime
    {
        public String ImageCat { get; set; } = "cat.png";
        public String ImageAA { get; set; } = "aa.png";
        public String ImagePlay { get; set; } = "play.png";
        public String ImageStop { get; set; } = "stop.png";
        public String ImageRewind { get; set; } = "rewind.png";
        public String ImageFastFoward { get; set; } = "fastfoward.png";
        public String ImageFastRewind { get; set; } = "fastrewind.png";

        private CCSprite sprite;
        private CCActionState state;
        private List<CCSpriteFrame> nekoSpriteFrames;
        private List<CCSpriteFrame> aaSpriteFrames;

        public CocosAnime() { }

        public void Create(object sender, EventArgs e)
        {
            var gameView = sender as CCGameView;
            var scene = new CCScene(gameView);
            var layer = new CCLayerColor();
            scene.AddLayer(layer);

            // 背景
            var background = new CCSprite("background.png", null);
            background.Position = new CCPoint(layer.ContentSize.Center.X, layer.ContentSize.Center.Y);
            layer.AddChild(background);

            // スプライトシートを読み込み、ファイル名で画像を昇順にソートする
            var nekoSpriteSheet = new CCSpriteSheet("neko.plist", "neko.png");
            var aaSpriteSheet = new CCSpriteSheet("aa.plist", "aa.png");
            nekoSpriteFrames = nekoSpriteSheet.Frames;
            aaSpriteFrames = aaSpriteSheet.Frames;
            nekoSpriteFrames.Sort((a, b) => a.TextureFilename.CompareTo(b.TextureFilename));
            aaSpriteFrames.Sort((a, b) => a.TextureFilename.CompareTo(b.TextureFilename));

            // スプライトにデフォルトの画像を設定
            sprite = new CCSprite(nekoSpriteFrames[0]);
            var scaleFactor = background.Texture.PixelsWide / sprite.ContentSize.Width;
            sprite.ScaleX = scaleFactor;
            sprite.ScaleY = scaleFactor;
            sprite.Position = new CCPoint(layer.ContentSize.Center.X, layer.ContentSize.Center.Y);
            layer.AddChild(sprite);

            gameView.RunWithScene(scene);
        }

        // ネコアニメに切り替え
        public void ChangeCatAnimation()
        {
            sprite.StopAction(state);
            sprite.SpriteFrame = nekoSpriteFrames[0];
        }

        // AAアニメに切り替え
        public void ChangeAAAnimation()
        {
            sprite.StopAction(state);
            sprite.SpriteFrame = aaSpriteFrames[0];
        }

        // 倍速逆再生 
        public void FastRewindAnimation()
        {
            sprite.StopAction(state);
            state = sprite.RepeatForever(new CCAnimate(new CCAnimation(ReverseSpriteFrames(sprite), 0.015f)));
        }

        // 逆再生
        public void RewindAnimation()
        {
            sprite.StopAction(state);
            state = sprite.RepeatForever(new CCAnimate(new CCAnimation(ReverseSpriteFrames(sprite), 0.03f)));
        }

        // 停止
        public void StopAnimation()
        {
            sprite.StopAction(state);
        }

        // 再生
        public void PlayAnimation()
        {
            sprite.StopAction(state);
            state = sprite.RepeatForever(new CCAnimate(new CCAnimation(SortSpriteFrames(sprite), 0.03f)));
        }

        // 倍速再生
        public void FastFowardAnimation()
        {
            sprite.StopAction(state);
            state = sprite.RepeatForever(new CCAnimate(new CCAnimation(SortSpriteFrames(sprite), 0.015f)));
        }

        // 引数のスプライト画像を先頭とする昇順のフレームリストを作成する
        private List<CCSpriteFrame> SortSpriteFrames(CCSprite sprite)
        {
            // sprite.SpriteFrame.TextureFileName : 消えてる…
            // sprite.Texture.Name : "neko" または "aa";
            // sprite.TextureRectInPixels : 矩形情報はスプライトシートの情報が残ってる！
            var sortedSpriteframe = new List<CCSpriteFrame>();
            var currentSpriteFrames = (sprite.Texture.Name.ToString() == "neko") ? nekoSpriteFrames : aaSpriteFrames;
            CCRect currentTextureRectInfo = sprite.TextureRectInPixels;
            int indexNo = currentSpriteFrames.FindIndex(x => x.TextureRectInPixels == currentTextureRectInfo);

            for (int i = indexNo; i < currentSpriteFrames.Count; i++){
                sortedSpriteframe.Add(currentSpriteFrames[i]);
            }

            int SubtractCount = currentSpriteFrames.Count - sortedSpriteframe.Count;
            for (int i = 0; i < SubtractCount; i++){
                sortedSpriteframe.Add(currentSpriteFrames[i]);
            }

            return sortedSpriteframe;
        }

        // 引数のスプライト画像を先頭とする降順のフレームリストを作成する
        private List<CCSpriteFrame> ReverseSpriteFrames(CCSprite sprite)
        {
            var sortedSpriteframe = new List<CCSpriteFrame>();
            var currentSpriteFrames = (sprite.Texture.Name.ToString() == "neko") ? nekoSpriteFrames : aaSpriteFrames;
            CCRect currentTextureRectInfo = sprite.TextureRectInPixels;
            int indexNo = currentSpriteFrames.FindIndex(x => x.TextureRectInPixels == currentTextureRectInfo);

            for (int i = indexNo; i >= 0; i--) {
                sortedSpriteframe.Add(currentSpriteFrames[i]);
            }

            int SubtractCount = sortedSpriteframe.Count;
            for (int i = currentSpriteFrames.Count - 1; i >= SubtractCount; i--) {
                sortedSpriteframe.Add(currentSpriteFrames[i]);
            }

            return sortedSpriteframe;
        }
    }
}
