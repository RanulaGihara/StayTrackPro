using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Data;
using StayTrackPro.Models;
using System.Text.RegularExpressions;

namespace StayTrackPro.Pages.Chatbot;

public class AssistantModel : PageModel
{
    [BindProperty]
    public string Query { get; set; }
    public string Response { get; set; }

    private readonly string[] greetings = { "hi", "hello", "hey", "good morning", "good afternoon", "good evening", "greetings" };
    private readonly string[] farewell = { "bye", "goodbye", "see you", "farewell", "thanks", "thank you" };
    

    private readonly Dictionary<string, string[]> roomKeywords = new()
    {
        { "deluxe", new[] { "deluxe", "luxury", "premium" } },
        { "standard", new[] { "standard", "regular", "basic", "normal" } },
        { "suite", new[] { "suite", "apartment", "room" } },
        { "executive", new[] { "executive", "business", "corporate" } },
        { "family", new[] { "family", "large", "big" } }
    };


    private readonly Dictionary<string, int> dateOffsets = new()
    {
        { "today", 0 },
        { "tomorrow", 1 },
        { "day after tomorrow", 2 },
        { "next week", 7 },
        { "this weekend", 0 } 
    };

    public void OnPost()
    {
        if (string.IsNullOrWhiteSpace(Query))
        {
            Response = "I'm here to help! Please ask me something about our rooms or availability. ";
            return;
        }

        var lower = Query.ToLower().Trim();
        

        if (IsGreeting(lower))
        {
            Response = GetGreetingResponse();
            return;
        }


        if (IsFarewell(lower))
        {
            Response = GetFarewellResponse();
            return;
        }


        if (IsHelpRequest(lower))
        {
            Response = GetHelpResponse();
            return;
        }

        
        if (IsGeneralQuestion(lower))
        {
            Response = HandleGeneralQuestion(lower);
            return;
        }

      
        if (IsAvailabilityQuestion(lower))
        {
            Response = HandleAvailabilityQuestion(lower);
            return;
        }

       
        if (IsPricingQuestion(lower))
        {
            Response = HandlePricingQuestion(lower);
            return;
        }

        if (IsRoomInfoQuestion(lower))
        {
            Response = HandleRoomInfoQuestion(lower);
            return;
        }

        Response = "I'm not sure I understand that. I can help you with:\n" +
                  "• Room availability (\"Is a deluxe suite available on Friday?\")\n" +
                  "• Pricing information (\"How much is a standard room?\")\n" +
                  "• Room details (\"Tell me about the executive suite\")\n" +
                  "• General hotel information\n\n" +
                  "What would you like to know? ";
    }

    private bool IsGreeting(string query)
    {
        return greetings.Any(g => query.Contains(g));
    }

    private bool IsFarewell(string query)
    {
        return farewell.Any(f => query.Contains(f));
    }

    private bool IsHelpRequest(string query)
    {
        return query.Contains("help") || query.Contains("what can you do") || 
               query.Contains("how can you help") || query.Contains("assist");
    }

    private bool IsGeneralQuestion(string query)
    {
        return query.Contains("about") || query.Contains("hotel") || 
               query.Contains("location") || query.Contains("amenities") ||
               query.Contains("facilities") || query.Contains("services");
    }

    private bool IsAvailabilityQuestion(string query)
    {
        return query.Contains("available") || query.Contains("free") || 
               query.Contains("vacancy") || query.Contains("book") ||
               query.Contains("reserve") || query.Contains("check in");
    }

    private bool IsPricingQuestion(string query)
    {
        return query.Contains("price") || query.Contains("cost") || 
               query.Contains("rate") || query.Contains("fee") ||
               query.Contains("how much") || query.Contains("charge");
    }

    private bool IsRoomInfoQuestion(string query)
    {
        return query.Contains("tell me about") || query.Contains("describe") || 
               query.Contains("features") || query.Contains("amenities") ||
               query.Contains("what's in") || query.Contains("room details");
    }

    private string GetGreetingResponse()
    {
        var responses = new[]
        {
            "Hello! Welcome to StayTrack Pro!  How can I assist you with your booking today?",
            "Hi there! I'm your booking assistant. What can I help you find today?",
            "Hey! Great to see you here. I'm ready to help with room availability, pricing, or any questions about our suites!",
            "Good to see you! I'm here to make your booking experience smooth. What would you like to know?"
        };
        
        return responses[new Random().Next(responses.Length)];
    }

    private string GetFarewellResponse()
    {
        var responses = new[]
        {
            "Thank you for using StayTrack Pro! Have a wonderful day! ",
            "Goodbye! Feel free to come back anytime if you need help with bookings. ",
            "Thanks for visiting! I hope I was helpful. See you soon! ",
            "Take care! Don't hesitate to return if you have more questions about our rooms."
        };
        
        return responses[new Random().Next(responses.Length)];
    }

    private string GetHelpResponse()
    {
        return "I'm your booking assistant! Here's what I can help you with:\n\n" +
               " **Room Availability**: Ask about specific dates and room types\n" +
               " **Pricing**: Get rate information for different suites\n" +
               " **Room Details**: Learn about amenities and features\n" +
               "**Booking Help**: Assistance with reservations\n\n" +
               "Try asking something like:\n" +
               "• \"Is a deluxe suite available this Friday?\"\n" +
               "• \"How much is a standard room?\"\n" +
               "• \"Tell me about the executive suite\"\n\n" +
               "What would you like to know? ";
    }

    private string HandleGeneralQuestion(string query)
    {
        if (query.Contains("location"))
        {
            return "We're conveniently located in the heart of the city!  Our hotel offers easy access to major attractions and business districts.";
        }
        
        if (query.Contains("amenities") || query.Contains("facilities"))
        {
            return "Our hotel features amazing amenities including:\n" +
                   " Swimming pool\n" +
                   "Restaurant and room service\n" +
                   "Business center\n" +
                   "Parking available\n" +
                   "Free WiFi throughout\n" +
                   "Fitness center\n\n" +
                   "Would you like to know about a specific room type?";
        }
        
        return "StayTrack Pro offers comfortable accommodations with excellent service!  " +
               "We have various suite types to meet your needs. What specific information would you like?";
    }

    private string HandleAvailabilityQuestion(string query)
    {
        var date = ExtractDate(query);
        var matchingSuites = ExtractRoomTypes(query);

        if (!matchingSuites.Any())
        {
            return "I'd be happy to check availability! Could you please specify which type of room you're interested in? " +
                   "We have deluxe, standard, executive, and family suites available. ";
        }

        foreach (var suite in matchingSuites)
        {
            var isBooked = AppMemoryContext.Reservations.Any(r =>
                r.SuiteId == suite.Id &&
                date >= r.ArrivalDate && date <= r.DepartureDate);

            if (!isBooked)
            {
                return $"Great news! The {suite.Type} suite \"{suite.SuiteName}\" is available on {date:dddd, MMMM d}. " +
                       $"Would you like me to help you with the booking process?";
            }
        }

        return $"I'm sorry, but the {matchingSuites[0].Type} suite is not available on {date:dddd, MMMM d}.  " +
               "Would you like me to suggest alternative dates or room types?";
    }

    private string HandlePricingQuestion(string query)
    {
        var matchingSuites = ExtractRoomTypes(query);
        
        if (!matchingSuites.Any())
        {
            return "I'd be happy to provide pricing information!  Could you specify which room type you're interested in? " +
                   "We have deluxe, standard, executive, and family suites with different rates.";
        }

        var suite = matchingSuites.First();
        var basePrice = GetSuitePrice(suite.Type);
        
        return $"The {suite.Type} suite \"{suite.SuiteName}\" starts at ${basePrice} per night. 💳\n" +
               "Rates may vary based on:\n" +
               "• Season and demand\n" +
               "• Length of stay\n" +
               "• Special promotions\n\n" +
               "Would you like to check availability for specific dates?";
    }

    private string HandleRoomInfoQuestion(string query)
    {
        var matchingSuites = ExtractRoomTypes(query);
        
        if (!matchingSuites.Any())
        {
            return "I'd love to tell you about our rooms!  Which suite type interests you?\n" +
                   "• Deluxe Suite - Luxury accommodations\n" +
                   "• Standard Suite - Comfortable and affordable\n" +
                   "• Executive Suite - Business-oriented features\n" +
                   "• Family Suite - Perfect for families";
        }

        var suite = matchingSuites.First();
        return GetRoomDescription(suite);
    }

    private List<Suite> ExtractRoomTypes(string query)
    {
        var matchingSuites = new List<Suite>();
        
        foreach (var (roomType, keywords) in roomKeywords)
        {
            if (keywords.Any(keyword => query.Contains(keyword)))
            {
                var suites = AppMemoryContext.Suites
                    .Where(s => s.Type.ToLower().Contains(roomType) || 
                               s.SuiteName.ToLower().Contains(roomType))
                    .ToList();
                matchingSuites.AddRange(suites);
            }
        }

        if (!matchingSuites.Any())
        {
            matchingSuites = AppMemoryContext.Suites
                .Where(s => query.Contains(s.Type.ToLower()) || 
                           query.Contains(s.SuiteName.ToLower()))
                .ToList();
        }

        return matchingSuites.Distinct().ToList();
    }

    private DateTime ExtractDate(string query)
    {
        var today = DateTime.Today;
        
        foreach (var (keyword, offset) in dateOffsets)
        {
            if (query.Contains(keyword))
            {
                if (keyword == "this weekend")
                {
                    return GetNextWeekend();
                }
                return today.AddDays(offset);
            }
        }

        var dayOfWeek = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>()
            .FirstOrDefault(d => query.Contains(d.ToString().ToLower()));

        if (dayOfWeek != default)
        {
            return GetNextDate(dayOfWeek);
        }

        var dateMatch = Regex.Match(query, @"\b(\d{1,2})(st|nd|rd|th)?\b");
        if (dateMatch.Success)
        {
            if (int.TryParse(dateMatch.Groups[1].Value, out int day) && day >= 1 && day <= 31)
            {
                try
                {
                    return new DateTime(today.Year, today.Month, day);
                }
                catch
                {
                    
                }
            }
        }

        return today;
    }

    private DateTime GetNextDate(DayOfWeek day)
    {
        var today = DateTime.Today;
        int offset = ((int)day - (int)today.DayOfWeek + 7) % 7;
        return today.AddDays(offset == 0 ? 7 : offset);
    }

    private DateTime GetNextWeekend()
    {
        var today = DateTime.Today;
        int daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7;
        return today.AddDays(daysUntilSaturday == 0 ? 7 : daysUntilSaturday);
    }

    private decimal GetSuitePrice(string suiteType)
    {
        return suiteType.ToLower() switch
        {
            "deluxe" => 199.99m,
            "executive" => 249.99m,
            "family" => 179.99m,
            "standard" => 129.99m,
            _ => 149.99m
        };
    }

    private string GetRoomDescription(Suite suite)
    {
        var descriptions = new Dictionary<string, string>
        {
            ["deluxe"] = "**Deluxe Suite** - Experience luxury at its finest!\n" +
                        "• Spacious layout with premium furnishings\n" +
                        "• King-size bed with premium linens\n" +
                        "• Marble bathroom with rainfall shower\n" +
                        "• City or garden views\n" +
                        "• Complimentary refreshments",
            
            ["standard"] = "**Standard Suite** - Comfortable and affordable!\n" +
                          "• Cozy and well-appointed room\n" +
                          "• Queen-size bed with quality linens\n" +
                          "• Modern bathroom with essential amenities\n" +
                          "• Work desk and seating area\n" +
                          "• Free WiFi and cable TV",
            
            ["executive"] = " **Executive Suite** - Perfect for business travelers!\n" +
                           "• Separate living and sleeping areas\n" +
                           "• Executive lounge access\n" +
                           "• High-speed internet and work station\n" +
                           "• Complimentary breakfast\n" +
                           "• Meeting room privileges",
            
            ["family"] = " **Family Suite** - Ideal for families!\n" +
                        "• Multiple bedrooms and common area\n" +
                        "• Kitchenette with essential appliances\n" +
                        "• Child-friendly amenities\n" +
                        "• Extra space for comfort\n" +
                        "• Connecting rooms available"
        };

        var suiteType = suite.Type.ToLower();
        foreach (var (key, description) in descriptions)
        {
            if (suiteType.Contains(key))
            {
                return description + $"\n\nWould you like to check availability for the {suite.SuiteName}?";
            }
        }

        return $"The {suite.SuiteName} is a wonderful {suite.Type} suite! " +
               "Would you like me to check availability or provide more specific information?";
    }
}