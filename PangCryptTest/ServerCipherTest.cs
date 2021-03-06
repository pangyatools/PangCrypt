using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PangCrypt;

namespace PangCryptTest
{
    [TestClass]
    public class ServerCipherTest
    {
        [TestMethod]
        public void TestCryptoGood()
        {
            var testCases = new List<(byte key, byte[] plain, byte[] cipher)>
            {
                (0, new byte[]
                {
                    0x96, 0x00, 0xFF, 0xE0, 0xF5, 0x05, 0x00, 0x00, 0x00, 0x00
                }, new byte[]
                {
                    0xEE, 0x13, 0x00, 0xFB, 0x00, 0x00, 0x00, 0x36, 0x1B, 0x96, 0x00, 0xF5, 0xFB, 0x63, 0x05, 0xFF,
                    0xE0, 0xF5, 0x05, 0x11, 0x00, 0x00
                }),

                (7, new byte[]
                {
                    0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00
                }, new byte[]
                {
                    0x49, 0x10, 0x00, 0x20, 0x00, 0x00, 0x00, 0x88, 0x18, 0x01, 0x00, 0x06, 0x18, 0x01, 0x00, 0x01,
                    0x11, 0x00, 0x00
                }),

                (5, new byte[]
                {
                    0x06, 0x00, 0x50, 0x61, 0x6e, 0x67, 0x79, 0x61, 0x21, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x50, 0x61, 0x6e, 0x67, 0x79, 0x61, 0x21, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x50, 0x61, 0x6e, 0x67, 0x79, 0x61, 0x21, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x50, 0x61, 0x6e, 0x67, 0x79, 0x61, 0x21, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x50, 0x61, 0x6e, 0x67, 0x79, 0x61, 0x21, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x50, 0x61, 0x6e, 0x67, 0x79, 0x61, 0x21, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x50, 0x61, 0x6e, 0x67, 0x79, 0x61, 0x21, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x50, 0x61, 0x6e, 0x67, 0x79, 0x61, 0x21, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00
                }, new byte[]
                {
                    0x23, 0x39, 0x00, 0xAA, 0x00, 0x00, 0x02, 0x13, 0x07, 0x06, 0x02, 0x14, 0x66, 0x68, 0x67, 0x29,
                    0x00, 0x4F, 0x67, 0x59, 0x76, 0x22, 0x00, 0x70, 0x76, 0x6D, 0x20, 0x50, 0xFC, 0x92, 0x20, 0x20,
                    0x96, 0x20, 0x07, 0x20, 0x09, 0xDC, 0x07, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x11, 0x00, 0x00
                }),

                (5, new byte[]
                {
                    0x01, 0x00, 0x00, 0x04, 0x00, 0x6a, 0x6f, 0x68, 0x6e, 0xb4, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x6a, 0x6f, 0x68,
                    0x6e, 0x74, 0x65, 0x73, 0x74, 0x00, 0x00
                }, new byte[]
                {
                    0x84, 0x2C, 0x00, 0x45, 0x00, 0x00, 0x00, 0x58, 0x09, 0x01, 0x00, 0x27, 0x0D, 0x01, 0x6A, 0x6F,
                    0x6C, 0x6E, 0xDE, 0x7F, 0x68, 0x8E, 0xB4, 0x10, 0x01, 0xE0, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                    0x08, 0x00, 0x6A, 0x6F, 0x60, 0x6E, 0x1E, 0x0A, 0x1B, 0x1A, 0x74, 0x65, 0x62, 0x74, 0x00
                }),

                (5, new byte[]
                {
                    0x01, 0x00, 0x00, 0x04, 0x00, 0x6a, 0x6f, 0x68, 0x6e, 0xb4, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x6a, 0x6f, 0x68,
                    0x6e, 0x74, 0x65, 0x73, 0x74, 0x00, 0x00
                }, new byte[]
                {
                    0x84, 0x2C, 0x00, 0x45, 0x00, 0x00, 0x00, 0x58, 0x09, 0x01, 0x00, 0x27, 0x0D, 0x01, 0x6A, 0x6F,
                    0x6C, 0x6E, 0xDE, 0x7F, 0x68, 0x8E, 0xB4, 0x10, 0x01, 0xE0, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                    0x08, 0x00, 0x6A, 0x6F, 0x60, 0x6E, 0x1E, 0x0A, 0x1B, 0x1A, 0x74, 0x65, 0x62, 0x74, 0x00
                })
            };

            foreach (var (key, plain, cipher) in testCases)
            {
                var decrypted = ServerCipher.Decrypt(cipher, key);
                CollectionAssert.AreEqual(plain, decrypted, "decrypt failed");

                var encrypted = ServerCipher.Encrypt(plain, key, cipher[0]);
                CollectionAssert.AreEqual(cipher, encrypted, "encrypt failed");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInvalidKeyEncrypt()
        {
            ServerCipher.Encrypt(new byte[] { }, 0x10, 0x00);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInvalidKeyDecrypt()
        {
            ServerCipher.Decrypt(new byte[] {0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01}, 0x10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInvalidBufferEmpty()
        {
            ServerCipher.Decrypt(new byte[] { }, 0x00);
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void TestInvalidBufferBadPayload()
        {
            ServerCipher.Decrypt(new byte[] {0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0xff}, 0x00);
        }
    }
}