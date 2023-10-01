﻿namespace NerdStore.Sales.Domain
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

           
        */
    }
}