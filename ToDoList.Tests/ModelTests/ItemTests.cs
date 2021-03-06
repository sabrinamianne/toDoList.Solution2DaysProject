using System.Collections.Generic;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using MySql.Data.MySqlClient;

namespace ToDoList.Tests
{
  [TestClass]
  public class ItemTest : IDisposable
  {
    public void Dispose()
    {
      Item.ClearAll();
    }

    public ItemTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=to_do_list_test;";
    }

    [TestMethod]
    public void ItemConstructor_CreatesInstanceOfItem_Item()
    {
      Item newItem = new Item("test", new DateTime(), 1);
      Assert.AreEqual(typeof(Item), newItem.GetType());
    }

    [TestMethod]
    public void GetDescription_ReturnDescription_String()
    {
      //Arrange
      string description = "Walk the dog.";
      Item newItem = new Item(description);
      //Act
      string result = newItem.GetDescription();
      // Assert
      Assert.AreEqual(description,result);
    }
    [TestMethod]
       public void SetDescription_SetDescription_String()
       {
         //Arrange
         string description = "Walk the dog.";
         Item newItem = new Item(description, new DateTime(), 1);

         //Act
         string updatedDescription = "Do the dishes";
         newItem.SetDescription(updatedDescription);
         string result = newItem.GetDescription();

         //Assert
         Assert.AreEqual(updatedDescription, result);
       }

       [TestMethod]
       public void GetAll_ReturnsEmptyList_ItemList()
       {
         //Arrange
         List<Item> newList = new List<Item> { };

         //Act
         List<Item> result = Item.GetAll();

         //Assert
         CollectionAssert.AreEqual(newList, result);
       }
       [TestMethod]
       public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
       {
         DateTime date = new DateTime();
         Item firstItem = new Item("Mow the lawn", date, 1);
         Item secondItem = new Item("Mow the lawn", date, 1);
         Assert.AreEqual(firstItem, secondItem);
       }

       [TestMethod]
       public void Save_SavesToDatabase_ItemList()
       {
         DateTime date = new DateTime();

         Item testItem = new Item("Mow the lawn", date, 1);
         testItem.Save();
         List<Item> result = Item.GetAll();
         List<Item> testList = new List<Item>{testItem};

         CollectionAssert.AreEqual(testList, result);
       }



       [TestMethod]
       public void GetAll_ReturnsItems_ItemList()
       {
         DateTime date = new DateTime();

         //Arrange
         string description01 = "Walk the dog";
         string description02 = "Wash the dishes";
         Item newItem1 = new Item(description01, date, 1);
         newItem1.Save();
         Item newItem2 = new Item(description02, date, 1);
         newItem2.Save();
         List<Item> newList = new List<Item> { newItem1, newItem2 };

         //Act
         List<Item> result = Item.GetAll();

         //Assert
         CollectionAssert.AreEqual(newList, result);
       }

      //  [TestMethod]
      // public void Save_AssignsIdToObject_Id()
      // {
      //   //Arrange
      //   Item testItem = new Item("Mow the lawn");
      //
      //   //Act
      //   testItem.Save();
      //   Item savedItem = Item.GetAll()[0];
      //
      //   int result = savedItem.GetId();
      //   int testId = testItem.GetId();
      //
      //   //Assert
      //   Assert.AreEqual(testId, result);
      // }





      [TestMethod]
      public void Find_ReturnsCorrectItemFromDatabase_Item()
      {
        DateTime date = new DateTime();

        //Arrange
        Item testItem = new Item("Mow the lawn", date, 1);
        testItem.Save();

        //Act
        Item foundItem = Item.Find(testItem.GetId());

        //Assert
        Assert.AreEqual(testItem, foundItem);
      }

      [TestMethod]
          public void Edit_UpdatesItemInDatabase_String()
          {
            //Arrange
            string firstDescription = "Walk the Dog";
            Item testItem = new Item(firstDescription);
            testItem.Save();
            string secondDescription = "Mow the lawn";

            //Act
            testItem.Edit(secondDescription);
            string result = Item.Find(testItem.GetId()).GetDescription();

            //Assert
            Assert.AreEqual(secondDescription, result);
          }



            [TestMethod]
              public void GetCategoryId_ReturnsItemsParentCategoryId_Int()
              {
                //Arrange
                Category newCategory = new Category("Home Tasks");
                Item newItem = new Item("Walk the dog.", newCategory.GetId());

                //Act
                int result = newItem.GetCategoryId();

                //Assert
                Assert.AreEqual(newCategory.GetId(), result);
              }

       // [TestMethod]
       // public void GetId_ItemsInstantiateWithAnIdAndGetterReturns_Int()
       // {
       //   //Arrange
       //   string description = "Walk the dog.";
       //   Item newItem = new Item(description);
       //
       //   //Act
       //   int result = newItem.GetId();
       //
       //   //Assert
       //   Assert.AreEqual(1, result);
       // }

       // [TestMethod]
       // public void Find_ReturnsCorrectItem_Item()
       // {
       //   //Arrange
       //   string description01 = "Walk the dog";
       //   string description02 = "Wash the dishes";
       //   Item newItem1 = new Item(description01);
       //   Item newItem2 = new Item(description02);
       //
       //   //Act
       //   Item result = Item.Find(2);
       //
       //   //Assert
       //   Assert.AreEqual(newItem2, result);
       // }

     }
   }
