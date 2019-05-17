# Dining-Philosophers
Classic Dining Philosophers 

Point of Interest:
1. IOutputter for multiple output form, implemented both ConsoleOutputter and Controller\EnumerableStringOutputter
2. AbstractOutput implemented lock for one thread one writeline at a time. Console.WriteLine is threadsafe, but other writer might not be.
2. PhilisopherFactory : FactoryBase<PhilosopherFactory> Singleton generic factory pattern
