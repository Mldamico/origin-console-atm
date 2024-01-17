using OriginConsole.Data;
using OriginConsole.Interfaces;
using OriginConsole.Repositories;
using OriginConsole.Views;

await Init.Execute();

ICardRepository cardRepository = new CardRepository();

var menu = new Start(cardRepository);
await menu.Display();


