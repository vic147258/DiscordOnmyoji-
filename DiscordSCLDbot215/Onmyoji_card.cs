using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordSCLDbot215
{
    class Onmyoji_card
    {
        private String _rarity = "N";
        private String _name = "";

        public String name
        {
            get { return _name; }
            set { _name = value; }
        }

        public String rarity
        {
            get { return _rarity; }
            set { _rarity = value; }
        }


        private String[] SP_pool;
        private String[] SSR_pool;
        private String[] SR_pool;
        private String[] R_pool;


        /*
        private String[] SP_pool =
        {
            "少羽大天狗",
            "煉獄茨木童子",
        };

        private String[] SSR_pool =
        {
            "花鳥卷",
            "白藏主",
            "鬼切",
            "面靈氣",
            "山風",
            "御饌津",
            "玉藻前",
            "雪童子",
            "彼岸花",
            "荒",
            "輝夜姬",
            "茨木童子",
            "一目連",
            "妖刀姬",
            "青行燈",
            "大天狗",
            "閻魔",
            "小鹿男",
            "酒吞童子",
            "荒川之主",
            "八岐大蛇"
        };

        private String[] SR_pool =
        {
            "桃花妖",
            "雪女",
            "鬼使白",
            "鬼使黑",
            "孟婆",
            "犬神",
            "骨女",
            "鬼女紅葉",
            "跳跳哥哥",
            "傀儡師",
            "海坊主",
            "判官",
            "鳳凰火",
            "吸血姬",
            "妖狐",
            "妖琴師",
            "食夢貘",
            "清姬",
            "鐮鼬",
            "姑獲鳥",
            "二口女",
            "白狼",
            "櫻花妖",
            "惠比壽",
            "絡新婦",
            "般若",
            "青坊主",
            "夜叉",
            "黑童子",
            "白童子",
            "煙煙羅",
            "金魚姬",
            "鴆",
            "以津真天",
            "匣中少女",
            "小松丸",
            "書翁",
            "百目鬼",
            "追月神",
            "日和坊",
            "薰",
            "弈",
            "貓掌櫃",
            "於菊蟲",
            "一反木綿",
            "入殮師"
        };

        private String[] R_pool =
        {
            "三尾狐",
            "座敷童子",
            "鯉魚精",
            "九命貓",
            "狸貓",
            "河童",
            "童男",
            "童女",
            "餓鬼",
            "巫蠱師",
            "鴉天狗",
            "食髮鬼",
            "武士之靈",
            "雨女",
            "跳跳弟弟",
            "跳跳妹妹",
            "兵俑",
            "丑時之女",
            "獨眼小僧",
            "鐵鼠",
            "椒圖",
            "管狐",
            "山兔",
            "螢草",
            "蝴蝶精",
            "山童",
            "首無",
            "覺",
            "青蛙瓷器",
            "古籠火",
            "蟲師"
        };
        */

        Random rnd;

        /// <summary>
        /// 一般抽卡
        /// </summary>
        public Onmyoji_card()
        {

        }

        public Onmyoji_card(String the_rarity, String the_name)
        {
            _rarity = the_rarity;
            _name = the_name;
        }

        public void read_card_pool()
        {
            SP_pool = System.IO.File.ReadAllLines("Onmyoji_SP.txt");
            SSR_pool = System.IO.File.ReadAllLines("Onmyoji_SSR.txt");
            SR_pool = System.IO.File.ReadAllLines("Onmyoji_SR.txt");
            R_pool = System.IO.File.ReadAllLines("Onmyoji_R.txt");
            rnd = new Random(Guid.NewGuid().GetHashCode());
        }


        public Onmyoji_card new_card(double rate)
        {

            double the_Probability = rnd.NextDouble();

            if (the_Probability <= 0.012035 * rate)  //測1千萬次的結果都有偏差 所以這邊補 0.000035  下面補0.00005
                return get_SP_SSR();

            if (the_Probability <= (0.20005 + 0.012))
                return get_SR();


            return get_R();

        }



        //中1.2%
        private Onmyoji_card get_SP_SSR()
        {

            //因為SP跟SSR機率都放在這1.2%裡面  所以要用卡池去平均分配
            if (rnd.NextDouble() * (SP_pool.Length + SSR_pool.Length) < SP_pool.Length)
                return new Onmyoji_card("SP", SP_pool[(int)(rnd.NextDouble() * SP_pool.Length)]);
            else
                return new Onmyoji_card("SSR", SSR_pool[(int)(rnd.NextDouble() * SSR_pool.Length)]);

        }


        //中20%
        private Onmyoji_card get_SR()
        {
            return new Onmyoji_card("SR", SR_pool[(int)(rnd.NextDouble() * SR_pool.Length)]);
        }


        //剩下
        private Onmyoji_card get_R()
        {
            return new Onmyoji_card("R", R_pool[(int)(rnd.NextDouble() * R_pool.Length)]);
        }
    }
}
