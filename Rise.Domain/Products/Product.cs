﻿using Rise.Domain.Leveranciers;
namespace Rise.Domain.Products;
public class Product : Entity
{
    private string name = default!;
    private string location = default!;
    private string description = default!;
    private bool reusable = default!;
    private int quantity = default!;
    private string barcode = default!;
    private Leverancier leverancier = default!;

    public required string Name
    {
        get => name;
        set => name = Guard.Against.NullOrWhiteSpace(value);
    }

    public required string Location
    {
        get => location;
        set => location = Guard.Against.NullOrWhiteSpace(value);
    }

    public required string Description
    {
        get => description;
        set => description = Guard.Against.NullOrWhiteSpace(value);
    }

    public required bool Reusable
    {
        get => reusable;
        set => reusable = value;
    }

    public required int Quantity
    {
        get => quantity;
        set => quantity = Guard.Against.Negative(value);
    }

    public required string Barcode
    {
        get => barcode;
        set => barcode = Guard.Against.NullOrWhiteSpace(value);
    }

    public required Leverancier Leverancier
    {
        get => leverancier;
        set
        {
            leverancier = Guard.Against.Null(value);
            leverancier.AddProduct(this);
        }
    }
}