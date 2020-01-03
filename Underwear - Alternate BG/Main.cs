using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace StorybrewScripts
{
    public class Lyric {
        [JsonProperty("sentence")] 
        public string sentence {get; set;}

        [JsonProperty("startTime")]
        public int startTime {get; set;}

        [JsonProperty("endTime")]
        public int endTime {get; set;}

        [JsonProperty("x")]
        public int posX {get; set;}

        [JsonProperty("y")]
        public int posY {get; set;}

    }

    public class Collab {
        [JsonProperty("mapper")] 
        public string mapper {get; set;}

        [JsonProperty("startTime")]
        public int startTime {get; set;}

        [JsonProperty("endTime")]
        public int endTime {get; set;} 

    }

    public class Main : StoryboardObjectGenerator
    {   
        [Configurable]
        public bool referesh = false;
        public bool IUnderstandSpritePoolsExistToSaveOsbSizeAndAreBadForPerformances = false;

        FontGenerator creditFont, lyricFont, crazyLyricFont, collabFont;
        public override void Generate()
        {
		    removeBackground();
            
            generateBackground();            
            generateCredits();
            generateLyrics();
            generateHits();

            if(Beatmap.Name == "Kim foo young's Expert") generateCollab();

            generateFlash();
            
        }

        #region Hitobject Stuff
        void generateHits()
        {
            generatePl();
            generatePP();
            generateRings();
        }


        void generatePl()
        {
            using (var pool = new OsbSpritePool(GetLayer("hl"), "sb/pl.png", OsbOrigin.Centre, (sprite, startTime, endTime) =>
            {

            }))
            {
                foreach(var hitobject in Beatmap.HitObjects)
                {
                    if((hitobject.StartTime >= 9390 - 5 && hitobject.StartTime < 9990 + 5) ||
                    (hitobject.StartTime >= 28582 - 5 && hitobject.StartTime < 29107 + 5) ||
                    (hitobject.StartTime >= 60381 - 5 && hitobject.StartTime < 67581 + 5) ||
                    (hitobject.StartTime >= 52582 - 5 && hitobject.StartTime < 53182 + 5)
                    )
                    {
                        var sprite = pool.Get(hitobject.StartTime, hitobject.EndTime + 200);
                        sprite.Scale(hitobject.StartTime, hitobject.StartTime + 100, 0,1);
                        sprite.Fade(hitobject.StartTime,1);
                        sprite.Color(hitobject.StartTime, hitobject.Color);
                        sprite.Fade(hitobject.EndTime, hitobject.EndTime + 200, 1, 0);
                        sprite.Move(hitobject.StartTime,hitobject.Position);
                        
                        if(hitobject is OsuSlider)
                        {
                            var pos = hitobject.Position;
                            for(var i = hitobject.StartTime; i <= hitobject.EndTime; i += tick(0,4))
                            {
                                sprite.Move(i, i + tick(0,4), hitobject.PositionAtTime(i), hitobject.PositionAtTime(i + tick(0,4)));
                            }
                        }
                    }
                }
            }
        }
        
        void generatePP()
        {
            using (var pool = new OsbSpritePool(GetLayer("hl"), "sb/pp.png", OsbOrigin.Centre, (sprite, startTime, endTime) =>
            {

            }))
            {
                foreach(var hitobject in Beatmap.HitObjects)
                {
                    if((hitobject.StartTime >= 33981 - 5 && hitobject.StartTime < 36381 + 5) ||
                    ((hitobject.StartTime >= 41181 - 5 && hitobject.StartTime < 42381 + 5) ||
                    (hitobject.StartTime >= 43581 - 5 && hitobject.StartTime < 52506 + 5) ||
                     (hitobject.StartTime >= 67582 - 5 && hitobject.StartTime < 76132 + 5))
                    )
                    {
                        for(var dotNum = 0; dotNum <= Random(5,20); dotNum++)
                        {
                            var angle = Random(0, Math.PI*2);
                            var radius = Random(50, 200);
                            var position = new Vector2(
                                hitobject.Position.X + (float)Math.Cos(angle) * radius,
                                hitobject.Position.Y + (float)Math.Sin(angle) * radius
                            );
                        
                            var sprite = pool.Get(hitobject.StartTime,hitobject.StartTime + 1000);
                            sprite.Move(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + 1000, hitobject.Position, position);
                            sprite.Fade(hitobject.StartTime, hitobject.StartTime + 1000, 1, 0);
                        }
                    }
                }

            }
        }

        void generateRings()
        {
            using (var pool = new OsbSpritePool(GetLayer("hl"), "sb/r.png", OsbOrigin.Centre, (sprite, startTime, endTime) =>
            {

            }))
            {
                foreach(var hitobject in Beatmap.HitObjects)
                {
                    if(
                        #region ringTimes
                    InTime(hitobject.StartTime,9990 ,9990)||
                    InTime(hitobject.StartTime,19590, 19590) ||
                    InTime(hitobject.StartTime,28881, 28881) ||
                    InTime(hitobject.StartTime,31582, 31582) ||
                    InTime(hitobject.StartTime,33981, 33981) ||
                    InTime(hitobject.StartTime,34281, 34281) ||
                    InTime(hitobject.StartTime,34581, 34581) ||
                    InTime(hitobject.StartTime,34881, 34881) ||
                    InTime(hitobject.StartTime,35181, 35181) ||
                    InTime(hitobject.StartTime,35481, 35481) ||
                    InTime(hitobject.StartTime,35781, 35781) ||
                    InTime(hitobject.StartTime,36081, 36081) ||
                    InTime(hitobject.StartTime,36381, 36381) ||
                    InTime(hitobject.StartTime,38782, 38782) ||
                    InTime(hitobject.StartTime,41181, 41181) ||
                    InTime(hitobject.StartTime,41481, 41481) ||
                    InTime(hitobject.StartTime,41781, 41781) ||
                    InTime(hitobject.StartTime,42081, 42081) ||
                    InTime(hitobject.StartTime,42381, 42381) ||
                    InTime(hitobject.StartTime,42681, 42681) ||
                    InTime(hitobject.StartTime,42981, 42981) ||
                    InTime(hitobject.StartTime,43281, 43281) ||
                    InTime(hitobject.StartTime,43581, 43581) ||
                    InTime(hitobject.StartTime,44781, 44781) ||
                    InTime(hitobject.StartTime,45981, 45981) ||
                    InTime(hitobject.StartTime,47181, 47181) ||
                    InTime(hitobject.StartTime,48381, 48381) ||
                    InTime(hitobject.StartTime,49582, 49582) ||
                    InTime(hitobject.StartTime,50782, 50782) ||
                    InTime(hitobject.StartTime,51982, 51982) ||
                    InTime(hitobject.StartTime,53182, 53182) ||
                    InTime(hitobject.StartTime,54382, 54382) ||
                    InTime(hitobject.StartTime,55582, 55582) ||
                    InTime(hitobject.StartTime,56782, 56782) ||
                    InTime(hitobject.StartTime,57982, 57982) ||
                    InTime(hitobject.StartTime,59182, 59182) ||
                    InTime(hitobject.StartTime,67582, 67582) 
                    
                    #endregion ringTimes
                    )
                    {
                        var sprite = pool.Get(hitobject.StartTime, hitobject.EndTime + 950);
                        sprite.Scale(OsbEasing.OutExpo,hitobject.StartTime, hitobject.StartTime + 950, 0,0.5);
                        
                        sprite.Fade(hitobject.StartTime, hitobject.StartTime + 950, 1, 0);
                        sprite.Move(hitobject.StartTime,hitobject.Position);
                    }

                }

            }
        }

#endregion Hitobject Stuff
        
        
        void generateBackground()
        {
            var blurBg = GetLayer("").CreateSprite("sb/blurBG.jpg");
            blurBg.Scale(0,854.0f/1024);
            blurBg.Fade(0,0);
            blurBg.Fade(390,0.6);
            blurBg.Fade(9990,0);

            var bwBG = GetLayer("").CreateSprite("sb/bwBG.jpg");
            bwBG.Scale(0,854.0f/1024);
            bwBG.Fade(0,0);
            bwBG.Fade(9990,0.45);
            bwBG.Fade(19590,0.6);
            bwBG.Fade(26781,0);
            bwBG.Fade(60381,0.45);
            bwBG.Fade(67581,0);

            var noGirl = GetLayer("").CreateSprite("sb/noGirl.png");
            noGirl.Scale(0,854.0f/1024);
            noGirl.Fade(0,0);
            noGirl.Fade(29181,1);
            noGirl.Fade(33756,33981,1,0.45);
            noGirl.Fade(36381,1);
            noGirl.Fade(43581,0);


            var girl = GetLayer("").CreateSprite("sb/girl.png",OsbOrigin.Centre,new Vector2(300, 240));
            girl.Scale(0,854.0f/970);
            girl.Fade(0,0);
            girl.Fade(29181,1);
            girl.Fade(33756,33981,1,0.45);
            girl.Fade(36381,1);
            girl.Fade(43581,0);

            // var currentDir = 0.0;
            // for(double start = 29181; start < 43581; start += tick(start,1) * 2)
            // {
            //     var newDir = girl.RotationAt(start) + Random(ConvertToRadians(-5),ConvertToRadians(2));
            //     girl.Rotate(start, start + tick(start,1) * 2,currentDir, newDir);
            //     currentDir = newDir;

            // }

            double newX;
            double newY;
            double newA;
            double lastX = girl.InitialPosition.X;
            double lastY = girl.InitialPosition.Y;
            double lastA = 0.0;
            
            for(int j = 29181; j <= 43581; j += (int)tick(29181, 0.5)){
                newX = 300+Random(-2.5, 2.5);
                newY = 240+Random(-2.5, 2.5);
                newA = Random(-1.5, 1.5);
                girl.Move(OsbEasing.InOutSine, j, j+tick(29181, 0.5), lastX, lastY, newX, newY);
                girl.Rotate(OsbEasing.InOutSine, j, j+tick(29181, 0.5), MathHelper.DegreesToRadians(lastA), MathHelper.DegreesToRadians(newA));
                lastX = newX;
                lastY = newY;
                lastA = newA;
            }




            // var rgbBG = GetLayer("").CreateSprite("sb/rgbBG.jpg");
            // rgbBG.Scale(0,854.0f/1024);
            // rgbBG.Fade(0,0);
            // rgbBG.Fade(67581,0.75);
            // rgbBG.Fade(76132,0);

            var rBG = GetLayer("").CreateSprite("sb/girlR.png");
            var gBG = GetLayer("").CreateSprite("sb/girlG.png");
            var bBG = GetLayer("").CreateSprite("sb/girlB.png");

            rBG.Scale(0,854.0f/1024);
            rBG.Fade(0,0);
            rBG.Fade(67581,0.75);
            rBG.Fade(76132,0);

            gBG.Scale(0,854.0f/1024);
            gBG.Fade(0,0);
            gBG.Fade(67581,0.75);
            gBG.Fade(76132,0);
            gBG.Additive(67581,76132);

            bBG.Scale(0,854.0f/1024);
            bBG.Fade(0,0);
            bBG.Fade(67581,0.75);
            bBG.Fade(76132,0);
            bBG.Additive(67581,76132);

            for(int j = 67581; j <= 74482; j += (int)tick(67581, 1))
            {
                var angleR = Random(0, Math.PI*2);
                var angleG = Random(0, Math.PI*2);
                var angleB = Random(0, Math.PI*2);
                var radiusR = Random(15, 30);
                var radiusG = Random(15, 30);
                var radiusB = Random(15, 30);

                var positionR = new Vector2(
                    320 + (float)Math.Cos(angleR) * radiusR,
                    240 + (float)Math.Sin(angleR) * radiusR
                );

                var positionG = new Vector2(
                    320 + (float)Math.Cos(angleG) * radiusG,
                    240 + (float)Math.Sin(angleG) * radiusG
                );

                var positionB = new Vector2(
                    320 + (float)Math.Cos(angleB) * radiusB,
                    240 + (float)Math.Sin(angleB) * radiusB
                );


                rBG.Move(OsbEasing.OutQuad, j,j + tick(67581, 1), new Vector2(320,240), positionR);
                gBG.Move(OsbEasing.OutQuad, j,j + tick(67581, 1), new Vector2(320,240), positionG);
                bBG.Move(OsbEasing.OutQuad, j,j + tick(67581, 1), new Vector2(320,240), positionB);

            }

            for(int j = 74782; j <= 75682; j += (int)tick(67581, 1) + (int)tick(67581, 2))
            {
                var angleR = Random(0, Math.PI*2);
                var angleG = Random(0, Math.PI*2);
                var angleB = Random(0, Math.PI*2);
                var radiusR = Random(15, 30);
                var radiusG = Random(15, 30);
                var radiusB = Random(15, 30);

                var positionR = new Vector2(
                    320 + (float)Math.Cos(angleR) * radiusR,
                    240 + (float)Math.Sin(angleR) * radiusR
                );

                var positionG = new Vector2(
                    320 + (float)Math.Cos(angleG) * radiusG,
                    240 + (float)Math.Sin(angleG) * radiusG
                );

                var positionB = new Vector2(
                    320 + (float)Math.Cos(angleB) * radiusB,
                    240 + (float)Math.Sin(angleB) * radiusB
                );


                rBG.Move(OsbEasing.OutQuad, j,j + (int)tick(67581, 1) + (int)tick(67581, 2), new Vector2(320,240), positionR);
                gBG.Move(OsbEasing.OutQuad, j,j + (int)tick(67581, 1) + (int)tick(67581, 2), new Vector2(320,240), positionG);
                bBG.Move(OsbEasing.OutQuad, j,j + (int)tick(67581, 1) + (int)tick(67581, 2), new Vector2(320,240), positionB);

            }

        }


        void generateFlash()
        {
            var flash = GetLayer("f").CreateSprite("sb/p.png");
            flash.ScaleVec(0,new Vector2(854,480));
            flash.Fade(0,0);

            flash.Fade(390,915,0.4,0);
            flash.Fade(9990,10365,0.4,0);
            flash.Fade(19590, 20115,0.4,0);

            flash.Fade(29182,29557,0.4,0);
            flash.Fade(36382,36907,0.4,0);
            flash.Fade(38782,39007,0.4,0);
            flash.Fade(43582,44107,0.4,0);
            flash.Fade(66982, 67582, 0,1);
            flash.Fade(67582,67807,0.4,0);
            flash.Fade(67582,68557,0.4,0);
            flash.Fade(69982,70807,0.4,0);

            flash.Fade(72382,72607,0.4,0);
            flash.Fade(73582,73807,0.4,0);
            flash.Fade(74782,75157,0.4,0);
            flash.Fade(75232,75607,0.4,0);
           
            flash.Fade(75682,76057,0.4,0);

            flash.Fade(76132,76807,1,0);
        }

        #region Credits
        void generateCredits()
        {
            creditFont = LoadFont("sb/f/c",new FontDescription(){
                FontPath="fonts/Montserrat-Light.ttf",
                Color=Color4.White
            });

            generateCreditFontTextures();

            var bar = GetLayer("").CreateSprite("sb/p.png");
            bar.ScaleVec(OsbEasing.OutExpo, 390, 840, new Vector2(0,5), new Vector2(450,5));
            bar.ScaleVec(OsbEasing.OutExpo, 9240, 9540, new Vector2(450,5), new Vector2(0,5));
            bar.Fade(390,1);
            bar.Fade(9540,0);

            //First Part
            var artistTexture = creditFont.GetTexture("Royal Republic");
            var titleTexture = creditFont.GetTexture("Underwear");

            //Half of second
            var diffTexture = creditFont.GetTexture(getDifficultyNames()[0]);
            var MappersTexture = creditFont.GetTexture(getDifficultyNames()[1]);

            var storyboardTexture = creditFont.GetTexture("Storyboard");
            var storyboarderTexture = creditFont.GetTexture("Coppertine");
            

            generateCredit(GetLayer(""),artistTexture,titleTexture,390,5190);
            generateCredit(GetLayer("hl"),diffTexture,MappersTexture,5190,7590);
            generateCredit(GetLayer(""),storyboardTexture,storyboarderTexture,7590,9390);


            

        }

        void generateCredit(StoryboardLayer layer, FontTexture topTexture, FontTexture bottomTexture, double start, double end)
        {
            var TopSprite = layer.CreateSprite(topTexture.Path);
            
            TopSprite.MoveY(start,240 - (topTexture.BaseHeight * 0.3) * 0.7);
            TopSprite.Fade(start, start + 250, 0, 1);
            TopSprite.Fade(end - 250, end, 1,0);
            TopSprite.Scale(start,0.3);
            TopSprite.MoveX(start, start + 250, 400, 325);  
            TopSprite.MoveX(start + 250, end - 250, 325, 300);
            TopSprite.MoveX(end - 250, end, 300, 250);

            // if(start != 16210)
            // {
            //     TopSprite.MoveX(start, start + 250, 400, 325);  
            //     TopSprite.MoveX(start + 250, end - 250, 325, 300);
            //     TopSprite.MoveX(end - 250, end, 300, 250);
            // }else{
            //  TopSprite.MoveX(start, start + 250, 400, 340);  
            //  TopSprite.MoveX(start + 250, 17260 , 340, 320);
                            
            // }

            var BottomSprite = layer.CreateSprite(bottomTexture.Path);
            
            BottomSprite.MoveY(start,240 + (bottomTexture.BaseHeight * 0.3)* 0.7);
            BottomSprite.Fade(start, start + 250, 0, 1);
            BottomSprite.Fade(end - 250, end, 1,0);
            BottomSprite.Scale(start,0.3);

            BottomSprite.MoveX(start, start + 250, 250, 300);  
            BottomSprite.MoveX(start + 250, end - 250, 300, 325);
            BottomSprite.MoveX(end - 250, end, 325, 400);
                
            // if(start != 16210)
            // {
            //     BottomSprite.MoveX(start, start + 250, 250, 300);  
            //     BottomSprite.MoveX(start + 250, end - 250, 300, 325);
            //     BottomSprite.MoveX(end - 250, end, 325, 400);
            // }else{
            //     BottomSprite.MoveX(start, start + 250, 250, 300);  
            //     BottomSprite.MoveX(start + 250, 17260, 300, 320);
            // }
        }

          string[] getDifficultyNames(){
            string[] MapStringList = {"",""};

            switch(Beatmap.Name)
            {
                case "GreenHue's Normal":
                    MapStringList[0] = "Normal";
                    MapStringList[1] = "GreenHue";
                    break;
                case "FrenZ's Hard":
                    MapStringList[0] = "Hard";
                    MapStringList[1] = "FrenZ396";
                    break;
                case "Hectique's Insane":
                    MapStringList[0] = "Insane";
                    MapStringList[1] = "Hectique";
                    break;
                case "NiNo's Insane":
                    MapStringList[0] = "Insane";
                    MapStringList[1] = "SnowNiNo_";
                    break;
                case "Scub's Insane":
                    MapStringList[0] = "Insane";
                    MapStringList[1] = "ScubDomino";
                    break;
                case "J1's Another":
                    MapStringList[0] = "Another";
                    MapStringList[1] = "J1_";
                    break;
                case "Kim foo young's Expert":
                    MapStringList[0] = "Expert";
                    MapStringList[1] = "Will Stetson | MBMasher";
                    break;
                case "MBStetson's Expert":
                    MapStringList[0] = "Expert";
                    MapStringList[1] = "Will Stetson | MBMasher";
                    break;
                case "Kalibe's Expert":
                    MapStringList[0] = "Expert";
                    MapStringList[1] = "Kalibe";
                    break;
                case "Reform's Extra":
                    MapStringList[0] = "Extra";
                    MapStringList[1] = "Reform";
                    break;
                case "Foxy's Extra":
                    MapStringList[0] = "Extra";
                    MapStringList[1] = "FoxyGrandpa";
                    break;
                case "Rolniczy's Extra":
                    MapStringList[0] = "Extra";
                    MapStringList[1] = "Rolniczy";
                    break;
                case "brotarks's Extra":
                    MapStringList[0] = "Extra";
                    MapStringList[1] = "brotarks";
                    break;
                case "OnlyBiscuit's Extra":
                    MapStringList[0] = "Extra";
                    MapStringList[1] = "OnlyBiscuit";
                    break;
                case "SMOKELIND's Extra":
                    MapStringList[0] = "Extra";
                    MapStringList[1] = "SMOKELIND";
                    break;
                case "Venix's Extra":
                    MapStringList[0] = "Extra";
                    MapStringList[1] = "Venix";
                    break;
                case "Polack's Extra":
                    MapStringList[0] = "Extra";
                    MapStringList[1] = "WORSTPOLACKEU";
                    break;
                case "Woey's Extra":
                    MapStringList[0] = "Extra";
                    MapStringList[1] = "Woey";
                    break;
                case "StunterLetsPlay's Extreme":
                    MapStringList[0] = "Extreme";
                    MapStringList[1] = "StunterLetsPlay";
                    break;
                case "IOException's Extreme":
                    MapStringList[0] = "Extreme";
                    MapStringList[1] = "IOException";
                    break;
                case "Ofredande!":
                    MapStringList[0] = "Ofredande!";
                    MapStringList[1] = "Will Stetson";
                    break;
                default:
                    MapStringList[0] = "Ofredande!";
                    MapStringList[1] = "Will Stetson";
                    break;
            }


            return MapStringList;
        }

       
        void generateCreditFontTextures()
        {
            List<string> textureStrings = new List<string> {"Normal", "GreenHue","Hard","FrenZ396","Insane","Hectique","SnowNiNo_","ScubDomino","Another","J1_","Expert","Will Stetson | MBMasher","Kalibe","Extra","Reform","FoxyGrandpa","Rolniczy","OnlyBiscuit","SMOKELIND","Venix","WORSTPOLACKEU","Woey","Extreme","StunterLetsPlay","IOException","Ofredande!","Will Stetson"};
            foreach(var textureString in textureStrings)
            {
                creditFont.GetTexture(textureString);
            }
        }

        #endregion
       
       void generateLyrics()
        {
            generateFonts();

            


            List<Lyric> lyrics = JsonConvert.DeserializeObject<List<Lyric>>(File.ReadAllText($"{ProjectPath}/lyrics.json"));

            foreach(var lyric in lyrics)
            {
                generateLyric(lyric);
            }           
            

        }

       void generateLyric(Lyric lyric)
        {
            var maroonColour = new Color4(128,0,0,255);
            var startingRot = ConvertToRadians(-15);
            var FontScale = 0.35f;
            var lineWidth = 0f;
            foreach(var character in lyric.sentence)
            {
                //var pos = new Vector2(lyric.posX,lyric.posY) + ;
                FontTexture texture = lyricFont.GetTexture(character.ToString());
                lineWidth += texture.BaseWidth * FontScale;               
            }
            var letterX = lyric.posX - lineWidth * 0.5f;

            foreach(var character in lyric.sentence)
            {
                FontTexture texture = lyricFont.GetTexture(character.ToString());
                if(!texture.IsEmpty)
                {
                    var position = new Vector2((float)Math.Cos(startingRot) * letterX  + lyric.posX, 
                                                (float)Math.Sin(startingRot) * letterX + lyric.posY)
                                + texture.OffsetFor(OsbOrigin.Centre) * FontScale;

                    //position = new Vector2(lyric.posX + (float)Math.Cos(letterX));
                    var sprite = GetLayer("").CreateSprite(texture.Path, OsbOrigin.Centre, position);
                    sprite.Rotate(lyric.startTime - 200,lyric.endTime,startingRot, ConvertToRadians(Random(-25,0)));
                    sprite.Scale(lyric.startTime, FontScale);
                    sprite.Fade(lyric.startTime - 200, lyric.startTime, 0, 1);
                    sprite.Fade(lyric.endTime - 200, lyric.endTime, 1, 0);
                    
                }
                letterX += texture.BaseWidth * FontScale;
            }


        }

       void generateFonts()
        {
            lyricFont = LoadFont("sb/f/l", new FontDescription(){
                FontPath = "fonts/NothingYouCouldDo.ttf",
                Color = Color4.White
            });
        }


        void generateCollab()
        {
            collabFont = LoadFont("sb/f/co", new FontDescription(){
                FontPath = "fonts/NothingYouCouldDo.ttf"
            });

            collabFont.GetTexture("Will Stetson");
            collabFont.GetTexture("MBMasher");
        }
       void removeBackground()
        {
            GetLayer("").CreateSprite(Beatmap.BackgroundPath).Fade(0,0);
        }
        

        double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        bool InTime(double starttime, double from, double to)
        {
            return starttime >= from  - 5 && starttime <= to+ 5;
        }

        double tick(double time,double divisor)
        {
            return Beatmap.GetTimingPointAt((int)time).BeatDuration / divisor;
        }
    }
}
