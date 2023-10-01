namespace NerdStore.Sales.Domain
{
    public class RoadMap
    {
        /* SALES DOMAIN DEVELOPMENT */

        /* ORDER - ITEM ORDER - VOUCHER */

        /*
            An item in an order represents a product and can contain more than one unit
            Regardless of the action, an item must always be valid:
                Possess:Product ID and Name, quantity between 1 and 15 units, value greater than 0

            An order while not started(payment process) is an draft status and must belong to a customer.

            1 - Add Item 
                1.1 - When adding an item it is necessary to calculate the total order value
                1.2 - If an item is already on the list then the quantity of the item must be added to the order
                1.3 - The item must have between 1 and 15 units of the product
           
            2 - Update Item 
                2.1 - The item must be in the list to be updated
                2.2 - An item can be upgraded containing more or fewer units than previously
                2.3 - When updating an item it is necessary to calculate the total order value
                2.4 - An item must remain between 1 and 15 units of product

            3 - Item Removal
                3.1 - The item must be on the list to be removed
                3.2 - When removing an item it is necessary to calculate the total order value

            A voucher has a unique code and the discount can be in percentage or fixed value
            Use a flag indicating that an order had a discount voucher applied and the amount of the discount generated

            4 - Apply discount voucher 
                4.1 - The voucher can only be applied if it is valid, for this reason:
                    4.1.1 - Must have a code
                    4.1.2 - The expiration date is greater than the current date
                    4.1.3 - The voucher is active
                    4.1.4 - The voucher possess a available quantity
                    4.1.5 - One of the discount forms must be filled in with a value above 0
                4.2 - Calculate the discount depending on the voucher type
                    4.2.1 - Voucher with percentage discount
                    4.2.2 - Voucher with discount in (real) values
                4.3 - When the discount value exceeds the order total, the order receives the value: 0
                4.4 - After applying the voucher, the discount must be re-calculated after any modification
                      to the list of order items
        */
    }
}