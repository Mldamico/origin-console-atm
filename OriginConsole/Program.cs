

using OriginConsole.Interfaces;
using OriginConsole.Servicios;
using OriginConsole.Views;

await Init.Execute();

ICardRepository cardRepository = new CardRepository();

var menu = new Menu(cardRepository);
await menu.ShowMenu();


