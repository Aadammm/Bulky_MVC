﻿using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        public readonly IUnitOfWork _unitfOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public CartController(IUnitOfWork unitfOfWork)
        {
            _unitfOfWork = unitfOfWork;
        }

        public IActionResult Index()
        {
            ClaimsIdentity? claimsIdentity = (ClaimsIdentity)User.Identity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new ShoppingCartVM()
            {
                ShoppingCartList = _unitfOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product")
            };

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                ShoppingCartVM.OrderTotal += (cart.Price * cart.Count);
            }
            return View(ShoppingCartVM);
        }

        public IActionResult Summary()
        {
            return View(); 
        }

        public IActionResult Plus(int cartId)
        {
            var cartDb = _unitfOfWork.ShoppingCart.Get(u => u.Id == cartId);
            cartDb.Count += 1;
            _unitfOfWork.ShoppingCart.Update(cartDb);
            _unitfOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cartDb = _unitfOfWork.ShoppingCart.Get(u => u.Id == cartId);
            if (cartDb.Count <= 1)
            {
                _unitfOfWork.ShoppingCart.Remove(cartDb);
            }
            else
            {
                cartDb.Count -= 1;
                _unitfOfWork.ShoppingCart.Update(cartDb);
            }
            _unitfOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cartDb = _unitfOfWork.ShoppingCart.Get(u => u.Id == cartId);

            _unitfOfWork.ShoppingCart.Remove(cartDb);
            _unitfOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
        private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            if (shoppingCart.Count <= 50)
            {
                return shoppingCart.Product.Price;
            }
            else
            {
                if (shoppingCart.Count <= 100)
                {
                    return shoppingCart.Product.Price50;
                }
                else
                {
                    return shoppingCart.Product.Price100;
                }
            }
        }
    }
}