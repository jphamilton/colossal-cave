﻿using System.Collections.Generic;

namespace Adventure.Net;

public class SaveGame
{
    public List<SaveObject> O { get; set; } = [];
    public List<int> I { get; set; } = [];
    public int L { get; set; }
    public int M { get; set; }
    public int CS { get; set; }
    public dynamic G { get; set; }
}
