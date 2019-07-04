using System.Collections.Generic;
using UnityEngine;

public class TextsGhosts {

    //////////////////////////////
    // ARTICLES
    //////////////////////////////

    public static List<string[]> GhostArticles = new List<string[]>();

    public static string[] GhostArticlesEN = {
        "Lorem",
        "Lorem",
        "Lorem"
    };

    public static string[] GhostArticlesDE = {
        "Enorm",
        "Lorem",
        "Lorem"
    };

    public static string[] GhostArticlesSW = {
        "Enorm",
        "Scho",
        "Recht",
        "Arg",
        "Brutal",
        "Sau",
        "Saumäßig",
        "Ein",
        "Dermaßen",
        "Super",
        "Extrem",
        "Sehr",
        "Auzoga",
        "Elendig"
    };


    //////////////////////////////
    // ADJECTIVES
    //////////////////////////////

    public static List<string[]> GhostAdjectives = new List<string[]>();

    public static string[] GhostAdjectivesEN = {
        "Lorem",
        "Lorem",
        "Lorem"
    };

    public static string[] GhostAdjectivesDE = {
        "Lorem",
        "Lorem",
        "Lorem"
    };

    public static string[] GhostAdjectivesSW = {
        "Bleeder",
        "Cleverer",
        "Trottliger",
        "Pimmliger",
        "Schneller",
        "Gscheiter",
        "Brutaler",
        "Auzogener",
        "Näckiger",
        "Seckliger",
        "Hobliger",
        "Dümmlicher",
        "Auneitiger",
        "Augriabiger",
        "Auleidiger",
        "Affiger",
        "Dämlicher",
        "Ranziger",
        "Rattiger",
        "Ralliger",
        "Lumpiger",
        "Dicker",
        "Dünner",
        "Farbiger",
        "Bunter",
        "Hohler",
        "Besoffener",
        "Versoffener",
        "Liadriger",
        "Verspielter",
        "Verhunzter",
        "Verlogener",
        "Unnötiger",
        "Kauziger",
        "Biestiger",
        "Nackter",
        "Siffiger",
        "Versiffter",
        "Intellektueller",
        "Gelehrter",
        "Kluger",
        "Studierter",
        "Belesener",
        "Gschickter",
        "Begabter",
        "Erfahrener",
        "Wilder",
        "Kräftiger",
        "Runder",
        "Eckiger",
        "Üppiger",
        "Stämmiger",
        "Harter",
        "Weicher",
        "Scharfer",
        "Schlechter",
        "Guter",
        "Grantiger",
        "Bruddeliger",
        "Gereizter",
        "Wütender",
        "Erboster",
        "Gwalttätiger",
        "Griesgrämiger",
        "Aufgebrachter",
        "Entrüsteter",
        "Aufdringlicher",
        "Unsympathischer",
        "Sympathischer",
        "Gemütlicher",
        "Herber",
        "Widerwilliger",
        "Zorniger",
        "Verärgerter",
        "Hässlicher",
        "Scheener",
        "Erregter",
        "Aggressiver",
        "Mürrischer",
        "Gehässiger",
        "Nörglerischer",
        "Schussliger",
        "Fetter",
        "Adipöser",
    };


    //////////////////////////////
    // NAMES
    //////////////////////////////

    public static List<string[]> GhostNames = new List<string[]>();

    public static string[] GhostNamesDE = {
        "Lorem",
        "Lorem",
        "Lorem"
    };

    public static string[] GhostNamesEN = {
        "Lorem",
        "Lorem",
        "Lorem"
    };

    public static string[] GhostNamesSW = {
        "Manu",
        "Benny",
        "Schwede",
        "Alex",
        "Marcel",
        "Thomas",
        "Jockel",
        "Depp",
        "Aff",
        "Nille",
        "Marc",
        "Bleedsau",
        "Seckel",
        "Lump",
        "Penner",
        "Ingenieur",
        "Hemmer",
        "Raket",
        "Fettsack",
        "Arschaffe",
        "Stinkesack",
    };


    //////////////////////////////
    //////////////////////////////
    //////////////////////////////


    public static void InitializeTexts() {
        Debug.Log("Adjectives: " + GhostAdjectivesSW.Length);
        Debug.Log("Names: " + GhostNamesSW.Length);
        int maxnames = GhostAdjectivesSW.Length * GhostNamesSW.Length;
        Debug.Log("Kombinationen: " + maxnames);

        GhostArticles.Add(GhostArticlesEN);
        GhostArticles.Add(GhostArticlesDE);
        GhostArticles.Add(GhostArticlesSW);

        GhostAdjectives.Add(GhostAdjectivesEN);
        GhostAdjectives.Add(GhostAdjectivesDE);
        GhostAdjectives.Add(GhostAdjectivesSW);

        GhostNames.Add(GhostNamesEN);
        GhostNames.Add(GhostNamesDE);
        GhostNames.Add(GhostNamesSW);
    }
}
