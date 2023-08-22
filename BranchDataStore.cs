using SimpleBank.API.Models;

namespace SimpleBank.API
{
	public class BranchDataStore
	{
		public List<BranchDto> branches { get; set; }

		public static BranchDataStore Current { get; } = new BranchDataStore();

		public BranchDataStore()
		{
			branches = new List<BranchDto>()
			{
				new BranchDto() { id = "BRSOM001", address = "Yaaqshid, Juungal", phone = "2091", 

                    tellers = new List<TellerDto>()
					{

						new TellerDto() { id = "TEL001", name = "Abdi Zamed Mohamed", phone = "618977249"}                  }
				},
				new BranchDto() { id = "BRSOM002", address = "Hodan, Taleex", phone = "2092",
					tellers = new List<TellerDto>()
					{
                        new TellerDto() { id = "TEL002", name = "Abdi Manan Mohamed", phone = "618977240"}
                    }
				},
				new BranchDto() { id = "BRSOM003", address = "Hodan, Isgooska Banaadir", phone = "2093",
					tellers = new List<TellerDto>()
					{
                        new TellerDto() { id = "TEL003", name = "Abdi Jimcale Barow", phone = "618977241"}
                    }
				},
            };
		}
    }
}

