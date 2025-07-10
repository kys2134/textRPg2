using System;
using System.Collections.Generic;

class Program
{
    private Profile profile = new Profile();
    private Store store;

    public Program()
    {
        profile = new Profile();
        profile.SetGame(this);
        store = new Store(profile, this);
    }
    static void Main(string[] args)
    {
        Program game = new Program();
        game.Gamestart();
    }

    public void Gamestart()
    {
        while (true)
        {
            Console.WriteLine(" 스파르타 마을에 오신 여러분 환영합니다.\n 이곳에서 던전으로 들어가기전 활동을 할 수 있습니다. \n 1. 상태 보기 \n 2. 인벤토리 \n 3. 상점 \n 0. 게임 종료 \n 원하시는 행동을 입력해주세요. \n >>");
            string input = Console.ReadLine();
            if (input == "1")
            {
                profile.ProfileOpen();
            }
            else if (input == "2")
            {
                Inventory inventory = new Inventory(profile.items, this);
                inventory.InventoryOpen();
            }
            else if (input == "3")
            {
                store.StoreOpen();
            }
            else if (input == "0")
            {
                Console.WriteLine("게임을 종료합니다.");
                break;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}

class Profile
{
    public int Lv = 1;
    public string Chad = "전사";
    public int 공격력 = 10;
    public int 방어력 = 5;
    public int 체력 = 100;
    public int Gold = 1500;
    public List<Item> items = new List<Item>();
    private Program game;

    public void SetGame(Program game)
    {
        this.game = game;
    }

    public void ProfileOpen()
    {
        int totalAtk = 공격력;
        int totalDef = 방어력;
        int atkBonus = 0;
        int defBonus = 0;
        foreach (Item item in items)
        {
            if (item.IsEquipped)
            {
                if (item.Type == "공격력")
                {
                    atkBonus += item.StatBoost;
                }
                if (item.Type == "방어력")
                {
                    defBonus += item.StatBoost;
                }
            }
        }
        totalAtk += atkBonus;
        totalDef += defBonus;

        Console.WriteLine("Lv." + Lv);
        Console.WriteLine("Chad (" + Chad + ")");
        Console.Write("공격력 : " + (공격력 + atkBonus));
        if (atkBonus > 0) Console.Write($" (+{atkBonus})");
        Console.WriteLine();

        Console.Write("방어력 : " + (방어력 + defBonus));
        if (defBonus > 0) Console.Write($" (+{defBonus})");
        Console.WriteLine();

        Console.WriteLine("체력 : " + 체력);
        Console.WriteLine("Gold : " + Gold);

        Console.WriteLine("0. 나가기");
        Console.WriteLine("원하시는 행동을 입력해주세요");
        Console.WriteLine(">>");
        string inputs = Console.ReadLine();
        if (inputs == "0")
        {
            return;
        }
    }
}

class Inventory
{
    private Program game;
    private List<Item> items;
    public Inventory(List<Item> items, Program program)
    {
        this.items = items;
        this.game = program;
    }
    public void InventoryOpen()
    {
        Console.WriteLine("인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n\n[아이템 목록]");
        ShowItems();

        Console.WriteLine("1. 장착 관리\n0. 나가기");

        Console.WriteLine("원하시는 행동을 입력해주세요\n>>");

        string input = Console.ReadLine();
        if (input == "1")
        {
            ToggleMenu();
        }
        else if (input == "0")
        {
            return;
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
            InventoryOpen();
        }

    }
    public void ShowItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            var item = items[i];
            string mark = item.IsEquipped ? "[E]" : "";
            Console.WriteLine($"{i + 1}. {mark}{item.Name,-10} | {item.Type,-4} +{item.StatBoost,-2} | {item.Description}");
        }
    }
    public void ToggleMenu()
    {
        Console.WriteLine("장착/해제할 아이템 번호를 입력하세요:");
        int index = int.Parse(Console.ReadLine()) - 1;
        if (index >= 0 && index < items.Count)
        {
            items[index].IsEquipped = !items[index].IsEquipped;
            Console.WriteLine(items[index].IsEquipped ? "장착되었습니다." : "해제되었습니다.");
            
        }
        else
        {
            return;
        }
    }
} 
class Item
    {
        public string Name;
        public string Description;
        public string Type;
        public int StatBoost;
        public bool IsEquipped;
        public int Price;
    }
 class Store
    {
        private List<Item> items = new List<Item>();
        private Profile profile;
        private Program program;


        public Store(Profile profile, Program program)
        {
            this.profile = profile;
            this.program = program;

            Item armor1 = new Item()
            {
                Name = "수련자의 갑옷",
                Description = "수련에 도움을 주는 갑옷입니다.",
                Type = "방어력",
                StatBoost = 5,
                IsEquipped = false,
                Price = 1000,
            };
            items.Add(armor1);

            Item armor2 = new Item()
            {
                Name = "무쇠갑옷",
                Description = "무쇠로 만들어져 튼튼한 갑옷입니다.",
                Type = "방어력",
                StatBoost = 9,
                IsEquipped = false,
                Price = 2000,
            };
            items.Add(armor2);

            Item armor3 = new Item()
            {
                Name = "스파르타의 갑옷",
                Description = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.",
                Type = "방어력",
                StatBoost = 15,
                IsEquipped = false,
                Price = 3500,
            };
            items.Add(armor3);

            Item weapon1 = new Item()
            {
                Name = "낡은 검",
                Description = "쉽게 볼 수 있는 낡은 검 입니다.",
                Type = "공격력",
                StatBoost = 2,
                IsEquipped = false,
                Price = 600,
            };
            items.Add(weapon1);

            Item weapon2 = new Item()
            {
                Name = "청동 도끼",
                Description = "어디선가 사용됐던거 같은 도끼입니다.",
                Type = "공격력",
                StatBoost = 5,
                IsEquipped = false,
                Price = 1500,
            };
            items.Add(weapon2);

            Item weapon3 = new Item()
            {
                Name = "스파르타의 창",
                Description = "스파르타의 전사들이 사용했다는 전설의 창입니다.",
                Type = "공격력",
                StatBoost = 7,
                IsEquipped = false,
                Price = 3500,
            };
            items.Add(weapon3);
        }
        public List<Item> GetItemList()
        {
            return items;
        }

        public void StoreOpen()
        {
            Console.WriteLine("상점\n 필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine(profile.Gold + "G");

            ShowItemsWithIndex();

            Console.WriteLine("1. 아이템 구매 \n0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.\n>>");
            string input = Console.ReadLine();
        if (input == "1")
        {
            BuyItem();
        }
        else if (input == "0")
        {
            return;
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
            StoreOpen();
            }

        }

        public void BuyItem()
        {
            Console.Write("구매할 아이템 번호 입력: ");
            int choice = int.Parse(Console.ReadLine());

            if (choice < 1 || choice > items.Count)
            {
                Console.WriteLine("잘못된 번호입니다.");
                return;
            }

            Item selectedItem = items[choice - 1];

            if (selectedItem.IsEquipped)
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
                return;
            }
            else if (profile.Gold >= selectedItem.Price)
            {
                profile.Gold -= selectedItem.Price;
                selectedItem.IsEquipped = true;
            profile.items.Add(selectedItem);
                Console.WriteLine("구매를 완료했습니다");

            }
            else
            {
                Console.WriteLine("Gold 가 부족합니다");
                return;
            }
        return;
        }

    public void ShowItemsWithIndex()
    {
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < items.Count; i++)
        {
            Item item = items[i];
            string mark = item.IsEquipped ? "[E] " : "";
            string priceText = item.IsEquipped ? "구매완료" : $"{item.Price}G";
            Console.WriteLine($"{i + 1}. {mark}{item.Name,-15} | {item.Type,-6} +{item.StatBoost,-2} | {item.Description,-40} | {priceText,-10}");
        }
        Console.WriteLine("플레이 해주셔서 감사합니다.");
        }
    }
