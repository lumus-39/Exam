using System;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text.Json;


namespace Exam
{
    class Program
    {
        static void Main(string[] args)
        {
            // экземпляр класса DictionaryManager
            var dictManager = new DictionaryManager();

            // путь к файлам словарей
            var enRuDictPath = "C:\\Dictionary\\en-ru-dictionary.json";
            var ruEnDictPath = "C:\\Dictionary\\ru-en-dictionary.json";

            // достаем словари из файлов
            var enRuDict = dictManager.ReadDictionaryFromFile(enRuDictPath);
            var ruEnDict = dictManager.ReadDictionaryFromFile(ruEnDictPath);

            // добавить новые слова в словарь enRuDict
            dictManager.AddWordToDictionary(enRuDict, "apple", "яблоко");
            dictManager.AddWordToDictionary(enRuDict, "car", "автомобиль");

            // удаляем слово "book" из словаря ruEnDict
            dictManager.RemoveWordFromDictionary(ruEnDict, "книга");

            // записываем словари обратно в файлы
            dictManager.WriteDictionaryToFile(enRuDict, enRuDictPath);
            dictManager.WriteDictionaryToFile(ruEnDict, ruEnDictPath);

            // выводим содержимое словаря enRuDict
            Console.WriteLine("Содержимое словаря enRuDict:");
            foreach (var word in enRuDict)
            {
                Console.WriteLine($"Слово: {word.Key}, перевод: {word.Value}");
            }

            // выводим содержимое словаря ruEnDict
            Console.WriteLine("Содержимое словаря ruEnDict:");
            foreach (var word in ruEnDict)
            {
                Console.WriteLine($"Слово: {word.Key}, перевод: {word.Value}");
            }

            Console.ReadLine();
        }
    }

    public class DictionaryManager
    {
        // Метод для чтения словаря из файла
        public Dictionary<string, string> ReadDictionaryFromFile(string filePath)
        {
            var dict = new Dictionary<string, string>();
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            }
            return dict;
        }

        // Метод для записи словаря в файл
        public void WriteDictionaryToFile(Dictionary<string, string> dict, string filePath)
        {
            var json = JsonSerializer.Serialize(dict);
            File.WriteAllText(filePath, json);
        }

        // Метод для добавления нового слова в словарь
        public void AddWordToDictionary(Dictionary<string, string> dict, string word, string translation)
        {
            if (!dict.ContainsKey(word))
            {
                dict.Add(word, translation);
            }
        }

        // Метод для удаления слова из словаря
        public void RemoveWordFromDictionary(Dictionary<string, string> dict, string word)
        {
            if (dict.ContainsKey(word))
            {
                dict.Remove(word);
            }
        }
    }
}