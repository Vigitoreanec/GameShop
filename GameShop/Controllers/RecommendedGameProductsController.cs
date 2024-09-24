using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameShopModel.Entities;
using GameShopModel.Repositories.Interface;
using GameShop.Repository.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameShop.Controllers;

public class RecommendedGameProductsController(IGameProductRepository gameProductRepository, IRecommendedGameProduct repositoryRecommendedGameProduct) : Controller
{
    public async Task<IActionResult> Index() =>
         View(await repositoryRecommendedGameProduct.GetAllAsync());
    
        // в этот момент получаем список, а способ зависит от систумы БД(отделились от конкретного способа получения)
   
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recommendedGameProduct = await repositoryRecommendedGameProduct.GetByIdAsync(id.Value);
        if (recommendedGameProduct == null)
        {
            return NotFound();
        }

        return View(recommendedGameProduct);
    }

    public async Task<IActionResult> Create()
    {
        var selectList = new SelectList(await gameProductRepository.GetAllGameProductsAsync());
        return View(selectList);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,ExpertName,ExpertSurname")] RecommendedGameProducts recommendedGameProducts)
    {
        if (ModelState.IsValid)
        {
            await repositoryRecommendedGameProduct.AddAsync(recommendedGameProducts);
            
            return RedirectToAction(nameof(Index));
        }
        return View(recommendedGameProducts);
    }

    // GET: RecommendedGameProducts/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recommendedGameProduct = await repositoryRecommendedGameProduct.GetByIdAsync(id.Value);
        if (recommendedGameProduct == null)
        {
            return NotFound();
        }
        return View(recommendedGameProduct);
    }

    // POST: RecommendedGameProducts/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,ExpertName,ExpertSurname")] RecommendedGameProducts recommendedGameProducts)
    {
        if (id != recommendedGameProducts.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
               await repositoryRecommendedGameProduct.EditAsync(id, recommendedGameProducts);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await RecommendedGameProductsExists(recommendedGameProducts.Id)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(recommendedGameProducts);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var recommendedGameProduct = await repositoryRecommendedGameProduct.GetByIdAsync(id.Value);
            
        if (recommendedGameProduct == null)
        {
            return NotFound();
        }

        return View(recommendedGameProduct);
    }

    // POST: RecommendedGameProducts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await repositoryRecommendedGameProduct.RemoveAsync(id);
        
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> RecommendedGameProductsExists(int id)
    {
        var recommendedGameProduct = await repositoryRecommendedGameProduct.GetByIdAsync(id);
        
        return recommendedGameProduct != null;
    }
}
