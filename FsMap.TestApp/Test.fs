namespace Widgets

module MyModule =
    type Bar =
        { Value1: string
          Value2: int option
          Value3: System.DateTime option }

    type Foo = { Bar: Bar }
