# KataCheckout Health Assured Technical Test
## Purpose
This is an implementation of the KataCheckout problem (see [here](http://codekata.com/kata/kata09-back-to-the-checkout/) for more details) submitted for the Health Assured Technical Test.

## Requirements
- .Net 10.0
- NUnit

### Set up
```bash
git clone https://github.com/tomporter1/KataCheckout.git
cd KataCheckout
dotnet test
```

## Extending this project
As the product catalogue is set up in such a way that it is a Dictionary of product names to `IProductPrice` objects.

To add more prices or offers to this project, any of the existing offers can be extended by creating a new class that inherits from them and implements the `CalculatePrice` method.

If you need to add a completely new offer, all you need to do is implement the `IProductPrice` interface then set it up in the product catalogue.