public ActionResult Create(Insuree insuree)
{
    if (ModelState.IsValid)
    {
        // Base monthly cost
        decimal quote = 50;

        // Age-based adjustment
        if (insuree.Age <= 18)
        {
            quote += 100;
        }
        else if (insuree.Age >= 19 && insuree.Age <= 25)
        {
            quote += 50;
        }
        else if (insuree.Age >= 26)
        {
            quote += 25;
        }

        // Car year-based adjustment
        if (insuree.CarYear < 2000)
        {
            quote += 25;
        }
        else if (insuree.CarYear > 2015)
        {
            quote += 25;
        }

        // Car Make/Model-based adjustment
        if (insuree.CarMake == "Porsche")
        {
            quote += 25;

            if (insuree.CarModel == "911 Carrera")
            {
                quote += 25; // Add an additional $25 if it's a Porsche 911 Carrera
            }
        }

        // Speeding ticket adjustment
        quote += insuree.SpeedingTickets * 10;

        // DUI adjustment
        if (insuree.HasDUI)
        {
            quote += quote * 0.25m; // Add 25% for DUI
        }

        // Full coverage adjustment
        if (insuree.FullCoverage)
        {
            quote += quote * 0.50m; // Add 50% for full coverage
        }

        // Assign calculated quote to the insuree
        insuree.Quote = quote;

        // Save to database
        db.Insurees.Add(insuree);
        db.SaveChanges();

        return RedirectToAction("Index");
    }

    return View(insuree);
}
