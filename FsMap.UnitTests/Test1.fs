﻿namespace FsMap.UnitTests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type Test1 () =

    [<TestMethod>]
    member this.TestMethodPassing () =
        Assert.IsTrue(true);